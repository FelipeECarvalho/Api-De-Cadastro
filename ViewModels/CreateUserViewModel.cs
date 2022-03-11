using System.ComponentModel.DataAnnotations;

namespace CadastroApi.ViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "The field Name is required")]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "The field Name must be between 3 and 80 caracters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field Cpf is required")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "The field Name must be between 3 and 30 caracters")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "The field E-mail is required")]
        [EmailAddress(ErrorMessage = "E-mail in a invalid format")]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "The field Name must be between 3 and 120 caracters")]
        public string Email { get; set; }
    }
}
