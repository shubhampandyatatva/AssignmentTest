using Assignment.Repository.ApplicationDbContext;
using Assignment.Repository.Data;
using Assignment.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Repository.Implementations;

public class AdminRepository : IAdminRepository
{
    private readonly AssignmentDbContext _dbcontext;
    public AdminRepository(AssignmentDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<List<Blog>> GetAllBlogs()
    {
        return await _dbcontext.Blogs.Where(b => b.Isdeleted != true).OrderBy(b => b.Id).ToListAsync();
    }

    public async Task<Blog> GetBlogById(int id)
    {
        return await _dbcontext.Blogs.FindAsync(id);
    }

    public async Task<bool> UpdateBlog(Blog blog)
    {
        try
        {
            _dbcontext.Blogs.Update(blog);
            await _dbcontext.SaveChangesAsync();
            return true;
        } catch (Exception e)
        {
            Console.WriteLine("Exception occured! " + e.Message);
            Console.WriteLine("Exception: "+ e);
            return false;
        }
    }

}
