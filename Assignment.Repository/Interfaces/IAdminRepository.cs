using Assignment.Repository.Data;

namespace Assignment.Repository.Interfaces;

public interface IAdminRepository
{
    Task<List<Blog>> GetAllBlogs();
    Task<Blog> GetBlogById(int id);
    Task<bool> UpdateBlog(Blog blog);

}
