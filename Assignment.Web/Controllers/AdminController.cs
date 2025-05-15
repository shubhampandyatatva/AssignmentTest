using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Assignment.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Assignment.Repository.ViewModels;
using System.Threading.Tasks;
using Assignment.Service.Interfaces;

namespace Assignment.Web.Controllers;

public class AdminController : Controller
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [Authorize(Roles = "1")]
    public async Task<IActionResult> Index(int page = 1, int pageSize = 5, string searchString = null)
    {
        var (blogList, totalBlogs) = await _adminService.GetBlogListViewModel(page, pageSize, searchString);

        // int totalBlogs = await _adminService.GetTotalBlogsCount();

        ViewBag.Page = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalBlogs = totalBlogs;

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return PartialView("_BlogListPartial", blogList);
        }

        return View(blogList);
    }

    [Authorize(Roles = "1")]
    public async Task<JsonResult> DeleteBlog(int id)
    {
        bool result = await _adminService.DeleteBlog(id);
        return Json(new { success = result });
    }

    [Authorize(Roles = "1")]
    public IActionResult CreateNewBlog()
    {
        return View();
    }

    [Authorize(Roles = "1")]
    public async Task<IActionResult> PublishBlog(AddEditBlogViewModel addEditBlogViewModel)
    {
        if (ModelState.IsValid)
        {
            bool result = await _adminService.SaveBlog(addEditBlogViewModel);
            if (result)
            {
                TempData["SuccessMessage"] = "Blog published successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Some error occured in publishing the blog";
                return View("CreateNewBlog");
            }
        }
        else
        {
            Console.WriteLine("Model state is not valid!");
            return View("CreateNewBlog");
        }
    }

    [Authorize(Roles = "1")]
    public async Task<IActionResult> EditBlog(int id)
    {
        TempData["BlogId"] = id;
        AddEditBlogViewModel blog = await _adminService.GetExistingBlogById(id);
        if (blog == null)
        {
            return NotFound();
        }
        return View(blog);
    }

    [Authorize(Roles = "1")]
    [HttpPost]
    public async Task<IActionResult> EditBlog(AddEditBlogViewModel addEditBlogViewModel)
    {
        if (ModelState.IsValid)
        {
            bool result = await _adminService.EditBlog(addEditBlogViewModel);
            if (result)
            {
                TempData["SuccessMessage"] = "Blog updated successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Some error occured in updating the blog";
                return View("EditBlog");
            }
        }
        else
        {
            Console.WriteLine("Model state is not valid!");
            return View("EditBlog");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
