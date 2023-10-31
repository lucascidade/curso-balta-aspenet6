using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models.ViewModels.Accounts
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "infomre o email")]
        [EmailAddress(ErrorMessage = "e-mail inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe a senha! ")]
        public string Password { get; set; }
    }
}
