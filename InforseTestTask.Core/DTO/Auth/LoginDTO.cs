using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Core.DTO.Auth
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email can't be null")]
        [EmailAddress(ErrorMessage = "Email adress should be in a proper email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password can't be null")]
        public string Password { get; set; } = string.Empty;
    }
}
