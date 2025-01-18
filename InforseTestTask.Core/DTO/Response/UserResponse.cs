using InforseTestTask.Core.Domain.Entityes.Indentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Core.DTO.Response
{
    public class UserResponse
    {
        public Guid Id;
        public string? Email { get; set; }

        public UserResponse(ApplicationUser user)
        {
            Id = user.Id;
            Email = user.Email;
        }
    }
}
