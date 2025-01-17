using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Core.DTO.Auth
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Email can't be null")]
        [EmailAddress(ErrorMessage = "Email adress should be in a proper email format")]
        [Remote(action: "IsEmailAlreadyRegister", controller: "Accounts", ErrorMessage = "Email already is used")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number can't be blank")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number should contain only digits")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password can't be blank")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm password can't be blank")]
        [Compare("Password", ErrorMessage = "Password and Confirm password should match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string[] Roles { get; set; }
    }
}
