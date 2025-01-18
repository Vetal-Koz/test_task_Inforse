using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Core.DTO.Auth
{
    public class AuthResposne
    {
        public string? Email { get; set; } = string.Empty;
        public string? Token { get; set; } = string.Empty;
        public List<string> Roles { get; set; }
        public DateTime Expiration { get; set; }
    }
}
