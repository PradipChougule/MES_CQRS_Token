namespace Manufacturing.Core.Domain
{
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<UserManagement> UserManagement { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRolesXref> UserRolesXref { get; set; }
    }
}
