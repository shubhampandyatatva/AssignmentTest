using Assignment.Repository.ApplicationDbContext;
using Assignment.Repository.Data;
using Assignment.Repository.Interfaces;

namespace Assignment.Repository.Implementations;

public class RolePermissionsRepository : IRolePermissionsRepository
{
    private readonly AssignmentDbContext _dbcontext;
    public RolePermissionsRepository(AssignmentDbContext assignmentDbContext)
    {
        _dbcontext = assignmentDbContext;
    }

    public async Task<string> FindRoleNameByRoleId(int userRoleId)
    {
        User user = await _dbcontext.Users.FindAsync(userRoleId);
        return user.Role.Name;
    }
}
