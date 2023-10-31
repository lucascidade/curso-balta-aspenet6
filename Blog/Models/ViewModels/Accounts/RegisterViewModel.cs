using System.ComponentModel.DataAnnotations;

namespace Blog.Models.ViewModels.Accounts
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Usuário é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail é inválido")]
        public string Email { get; set; }
    }
}
