using Assignment.Repository.ApplicationDbContext;
using Assignment.Repository.Interfaces;

namespace Assignment.Repository.Implementations;

public class AuthorizationRepository : IAuthorizationRepository
{
    private readonly AssignmentDbContext _dbcontext;
    public AuthorizationRepository(AssignmentDbContext assignmentDbContext)
    {
        _dbcontext = assignmentDbContext;
    }
}
