using InforseTestTask.Core.Domain.Entityes;
using InforseTestTask.Core.Domain.Entityes.Indentity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Infastructure
{
    public class InforseDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public InforseDBContext(DbContextOptions options) : base(options) { }

        public DbSet<ShortUrl> ShortUrls { get; set; }
        public DbSet<AboutInfo> AboutInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(InforseDBContext).Assembly);
            base.OnModelCreating(builder);

            var userRoleId = Guid.NewGuid();
            var adminRoleId = Guid.NewGuid();

            var roles = new List<ApplicationRole>
            {
                new ApplicationRole
                {
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId.ToString(),
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                },
                new ApplicationRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId.ToString(),
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
            };

            builder.Entity<ApplicationRole>().HasData(roles);
        }
    }
}
