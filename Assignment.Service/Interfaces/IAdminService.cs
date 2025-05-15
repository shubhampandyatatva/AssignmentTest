using Assignment.Repository.ViewModels;

namespace Assignment.Service.Interfaces;

public interface IAdminService
{
    Task<bool> DeleteBlog(int id);
    Task<(List<BlogListViewModel>, int)> GetBlogListViewModel(int page, int pageSize, string searchString);
    Task<bool> SaveBlog(AddEditBlogViewModel addEditBlogViewModel);
    Task<bool> EditBlog(AddEditBlogViewModel addEditBlogViewModel);
    Task<AddEditBlogViewModel> GetExistingBlogById(int id);
    Task<int> GetTotalBlogsCount();
}
