using InforseTestTask.Core.Domain.Entityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Infastructure.Configurations
{
    public class ShortUrlConfiguration : IEntityTypeConfiguration<ShortUrl>
    {
        public void Configure(EntityTypeBuilder<ShortUrl> builder)
        {
            builder
                .HasIndex(su => su.ShortenedUrl)
                .IsUnique();
        }
    }
}
