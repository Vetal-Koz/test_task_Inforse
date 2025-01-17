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
    public class InforseDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, long>
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
        }
    }
}
