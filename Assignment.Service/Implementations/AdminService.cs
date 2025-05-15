using Assignment.Repository.Data;
using Assignment.Repository.Interfaces;
using Assignment.Repository.ViewModels;
using Assignment.Service.Interfaces;

namespace Assignment.Service.Implementations;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _adminRepository;
    public AdminService(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }

    public async Task<List<BlogListViewModel>> GetBlogListViewModel()
    {
        List<Blog> blogs = await _adminRepository.GetAllBlogs();
        List<BlogListViewModel> blogList = new();
        foreach(Blog blog in blogs)
        {
            blogList.Add(new BlogListViewModel{
                Id = blog.Id,
                BlogTitle = blog.Title,
                BlogContent = blog.Content,
                PublishedDate = blog.Publisheddate
            });
        }

        return blogList;
    }

    public async Task<bool> DeleteBlog(int id)
    {
        Blog blog = await _adminRepository.GetBlogById(id);
        if(blog == null)
        {
            Console.WriteLine("Blog not found by this ID!");
            return false;
        }

        blog.Isdeleted = true;
        bool isUpdated = await _adminRepository.UpdateBlog(blog);
        if(!isUpdated)
        {
            Console.WriteLine("Blog not updated!");
        }
        return isUpdated;
    }

}
