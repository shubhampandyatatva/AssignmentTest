using Assignment.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Repository.ApplicationDbContext;

public class AssignmentDbContext : DbContext
{
    public AssignmentDbContext(DbContextOptions<AssignmentDbContext> options) : base(options)
    {
    }

    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Blog> Blogs { get; set; }
}
