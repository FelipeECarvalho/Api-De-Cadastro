using System.ComponentModel.DataAnnotations;

namespace CadastroApi.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The field E-mail is required")]
        [EmailAddress(ErrorMessage = "E-mail in a invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field Password is required")]
        public string Password { get; set; }
    }
}
