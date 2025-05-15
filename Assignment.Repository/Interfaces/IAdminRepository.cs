using Assignment.Repository.Data;

namespace Assignment.Repository.Interfaces;

public interface IAdminRepository
{
    Task<bool> AddBlog(Blog blog);

    Task<List<Blog>> GetAllBlogs(string searchString);
    Task<Blog> GetBlogById(int id);
    Task<int> GetTotalBlogsCount();

    Task<bool> UpdateBlog(Blog blog);

}
