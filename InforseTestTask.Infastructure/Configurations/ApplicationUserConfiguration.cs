using InforseTestTask.Core.Domain.Entityes.Indentity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Infastructure.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .HasMany(user => user.createdUrls)
                .WithOne(url => url.CreatedBy)
                .HasForeignKey("UserId")
                .IsRequired();
        }
    }
}
