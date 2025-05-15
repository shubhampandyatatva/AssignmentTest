using Assignment.Repository.ViewModels;

namespace Assignment.Service.Interfaces;

public interface IAdminService
{
    Task<bool> DeleteBlog(int id);

    Task<List<BlogListViewModel>> GetBlogListViewModel();

}
