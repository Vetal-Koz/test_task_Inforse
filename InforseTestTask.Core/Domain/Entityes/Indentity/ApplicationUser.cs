using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Core.Domain.Entityes.Indentity
{
    public class ApplicationUser : IdentityUser<long>
    {
        public ISet<ShortUrl> createdUrls { get; set; } = new HashSet<ShortUrl>();
    }
}
