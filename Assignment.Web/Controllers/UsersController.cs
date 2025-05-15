using System.Security.Claims;
using Assignment.Repository.Data;
using Assignment.Repository.ViewModels;
using Assignment.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Web.Controllers;

public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly IAdminService _adminService;
    private readonly IJWTService _jWTService;
    public UsersController(IUserService userService, IAdminService adminService, IJWTService jWTService)
    {
        _userService = userService;
        _adminService = adminService;
        _jWTService = jWTService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int page = 1, int pageSize = 5, string searchString = null)
    {
        var (blogList, totalBlogs) = await _adminService.GetBlogListViewModel(page, pageSize, searchString);
        ViewBag.Page = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalBlogs = totalBlogs;
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return PartialView("_BlogListPartial", blogList);
        }
        return View(blogList);
    }

    public async Task<IActionResult> ViewBlog(int id)
    {
        ViewBlogModel blog = await _userService.GetAddEditBlogViewModel(id);
        if (blog == null)
        {
            return NotFound();
        }
        return View(blog);
    }

    public async Task<JsonResult> AddComment(string comment, int blogId)
    {
        string jwtToken = Request.Cookies["JwtToken"];
        if (string.IsNullOrEmpty(jwtToken))
        {
            Console.WriteLine("JWT token is null or empty");
        }
        var userId = _jWTService.GetClaimValue(jwtToken, ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Json(new { success = false, message = "User not found" });
        }
        bool result = await _userService.AddComment(comment, blogId, userId);
        if (result)
        {
            return Json(new { success = true, message = "Comment added successfully" });
        }
        return Json(new { success = false, message = "Failed to add comment" });
    }

    public async Task<IActionResult> GetCommentsByBlogId(int id)
    {
        List<CommentViewModel> comments = await _userService.GetCommentViewModelByBlogId(id);
        return PartialView("_CommentListPartial", comments);
    }
}
