using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BOILoanPortal.Models
{
    public class LoginModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }


        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public string? RememberMe { get; set; }
    }
}
