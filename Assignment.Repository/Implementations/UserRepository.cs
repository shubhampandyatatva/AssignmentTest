using Assignment.Repository.ApplicationDbContext;
using Assignment.Repository.Data;
using Assignment.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Repository.Implementations;

public class UserRepository : IUserRepository
{
    private readonly AssignmentDbContext _dbcontext;
    public UserRepository(AssignmentDbContext assignmentDbContext)
    {
        _dbcontext = assignmentDbContext;
    }

    public async Task<User> FindUserByEmail(string email)
    {
        List<User> users = await _dbcontext.Users.ToListAsync();
        User user = await _dbcontext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task<Blog> GetBlogById(int id)
    {
        return await _dbcontext.Blogs.FirstOrDefaultAsync(b => b.Id == id);
    }

    public Task<List<Comment>> GetCommentsByBlogId(int id)
    {
        return _dbcontext.Comments.Where(c => c.Blogid == id).OrderBy(c => c.Id).ToListAsync();
    }

    public async Task<bool> AddComment(Comment newComment)
    {
        try
        {
            _dbcontext.Comments.Add(newComment);
            _dbcontext.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
            return false;
        }
    }

}
