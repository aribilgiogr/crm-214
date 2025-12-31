using System.ComponentModel.DataAnnotations;

namespace Core.Concretes.DTOs
{
    public class LoginDTO
    {
        [Required, Display(Name = "Email Address", Prompt = "Email Address"), EmailAddress]
        public string Email { get; set; } = null!;

        [Required, Display(Name = "Password", Prompt = "Password"), DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "Remember Me", Prompt = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
