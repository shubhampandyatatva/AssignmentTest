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

}
