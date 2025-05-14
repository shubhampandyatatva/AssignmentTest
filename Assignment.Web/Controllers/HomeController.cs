using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Assignment.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace Assignment.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize(Roles = "User")]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
