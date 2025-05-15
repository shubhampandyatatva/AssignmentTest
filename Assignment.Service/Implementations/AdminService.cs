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

    public async Task<(List<BlogListViewModel>, int)> GetBlogListViewModel(int page, int pageSize, string searchString)
    {
        List<Blog> blogs = await _adminRepository.GetAllBlogs(searchString);
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

        int totalBlogs = blogs.Count;

        blogList = blogList.Skip((page - 1) * pageSize).Take(pageSize).ToList();       

        return (blogList, totalBlogs);
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

    public async Task<bool> SaveBlog(AddEditBlogViewModel addEditBlogViewModel)
    {
        Blog blog = new()
        {
            Title = addEditBlogViewModel.Title,
            Content = addEditBlogViewModel.Body,
            Publisheddate = DateTime.UtcNow,
            Tags = addEditBlogViewModel.Tags,
            Isdeleted = false
        };

        bool isAdded = await _adminRepository.AddBlog(blog);
        if(!isAdded)
        {
            Console.WriteLine("Blog not added!");
        }
        return isAdded;
    }

    public async Task<AddEditBlogViewModel> GetExistingBlogById(int id)
    {
        Blog blog = await _adminRepository.GetBlogById(id);
        if (blog == null)
        {
            Console.WriteLine("Blog not found by this ID!");
            return null;
        }
        
        AddEditBlogViewModel viewModel = new()
        {
            Id = blog.Id,
            Title = blog.Title,
            Body = blog.Content,
            Tags = blog.Tags
        };
        return viewModel;
    }

    public async Task<bool> EditBlog(AddEditBlogViewModel addEditBlogViewModel)
    {
        Blog blog = await _adminRepository.GetBlogById(addEditBlogViewModel.Id);
        if (blog == null)
        {
            Console.WriteLine("Blog not found by this ID!");
            return false;
        }

        blog.Title = addEditBlogViewModel.Title;
        blog.Content = addEditBlogViewModel.Body;
        blog.Publisheddate = DateTime.UtcNow;
        blog.Tags = addEditBlogViewModel.Tags;

        bool isUpdated = await _adminRepository.UpdateBlog(blog);
        if (!isUpdated)
        {
            Console.WriteLine("Blog not updated!");
        }
        return isUpdated;
    }

    public async Task<int> GetTotalBlogsCount()
    {
        return await _adminRepository.GetTotalBlogsCount();
    }

}
