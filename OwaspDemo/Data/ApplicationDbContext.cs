using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OwaspDemo.Data.Models;

namespace OwaspDemo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Login> Logins { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<StolenSession> StolenSessions { get; set; }
    }
}
