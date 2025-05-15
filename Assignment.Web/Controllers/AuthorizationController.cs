using System.Security.Claims;
using Assignment.Repository.Data;
using Assignment.Repository.ViewModels;
using Assignment.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Assignment.Web.Controllers;

public class AuthorizationController : Controller
{
    private readonly IUserService _userService;
    private readonly IAuthorizationService _authorizationService;
    private readonly IJWTService _jwtService;
    public AuthorizationController(IUserService userService, IAuthorizationService authorizationService, IJWTService jWTService)
    {
        _userService = userService;
        _authorizationService = authorizationService;
        _jwtService = jWTService;
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        string? jwtToken = Request.Cookies["JwtToken"];
        if (jwtToken == null)
        {
            return View();
        }

        string email = _jwtService.GetClaimValue(jwtToken, ClaimTypes.Email);
        User existingUser = await _userService.GetUserByEmail(email);
        if (existingUser == null)
        {
            return View();
        }
        string roleId = _jwtService.GetClaimValue(jwtToken, ClaimTypes.Role);
        if (roleId == "1")
        {
            return RedirectToAction("Index", "Admin");
        }
        else
        {
            return RedirectToAction("Index", "Users");
        }
    } 

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (ModelState.IsValid)
        {
            User user = await _userService.GetUserByEmail(loginViewModel.Email);
            if (user == null)
            {
                ViewBag.ErrorMessage = "User with this email does not exist!";
                return View();
            }

            bool result = await _authorizationService.ValidatePassword(loginViewModel.Email, loginViewModel.Password);
            if (!result)
            {
                ViewBag.ErrorMessage = "Email and Password do not match!";
                return View();
            }

            string token = await _jwtService.GenerateJwtToken(user);

            if (!loginViewModel.RememberMe)
            {
                CookieOptions cookieOptions = new()
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddHours(24)
                };
                Response.Cookies.Append("JwtToken", token, cookieOptions);
                Response.Cookies.Append("Email", loginViewModel.Email, cookieOptions);
            }
            else
            {
                CookieOptions cookieOptions = new()
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddDays(7)
                };
                Response.Cookies.Append("JwtToken", token, cookieOptions);
                Response.Cookies.Append("Email", loginViewModel.Email, cookieOptions);
            }

            TempData["SuccessMessage"] = "Successfully Logged In!";

            string roleId = _jwtService.GetClaimValue(token, ClaimTypes.Role);
            if (roleId == "1")
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "Users");
            }
        }
        else
        {
            TempData["ErrorMessage"] = "Model State is not valid!";
            return View();
        }
    }

    public IActionResult Logout()
    {
        if (Request.Cookies["Email"] != null)
        {
            Response.Cookies.Delete("Email");
        }

        if (Request.Cookies["JwtToken"] != null)
        {
            Response.Cookies.Delete("JwtToken");
        }

        if (Request.Cookies["ProfileImagePath"] != null)
        {
            Response.Cookies.Delete("ProfileImagePath");
        }

        return RedirectToAction(nameof(Login));
    }
}
