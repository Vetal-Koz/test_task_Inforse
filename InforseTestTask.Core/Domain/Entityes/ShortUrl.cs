using InforseTestTask.Core.Domain.Entityes.Indentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Core.Domain.Entityes
{
    public class ShortUrl
    {
        public long Id { get; set; }
        public string OriginalUrl { get; set; } = null!;
        public string ShortenedUrl { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public ApplicationUser CreatedBy { get; set; }
    }
}
