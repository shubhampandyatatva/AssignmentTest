using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Assignment.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Assignment.Repository.ViewModels;
using System.Threading.Tasks;
using Assignment.Service.Interfaces;

namespace Assignment.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAdminService _adminService;

    public HomeController(ILogger<HomeController> logger, IAdminService adminService)
    {
        _logger = logger;
        _adminService = adminService;
    }

    [Authorize(Roles = "1")]
    public async Task<IActionResult> Index()
    {
        List<BlogListViewModel> blogList = await _adminService.GetBlogListViewModel();
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return PartialView("_BlogListPartial", blogList);
        }
        return View(blogList);
    }

    public async Task<JsonResult> DeleteBlog(int id)
    {
        bool result = await _adminService.DeleteBlog(id);
        return Json(new {success = result});
    }

    public IActionResult CreateNewBlog()
    {
        return View();
    }

    [Authorize(Roles = "2")]
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
