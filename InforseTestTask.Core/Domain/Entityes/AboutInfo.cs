using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Core.Domain.Entityes
{
    public class AboutInfo
    {
        public long Id { get; set; }
        public string Description { get; set; } = null!;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
