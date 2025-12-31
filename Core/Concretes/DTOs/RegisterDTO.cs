using System.ComponentModel.DataAnnotations;

namespace Core.Concretes.DTOs
{
    public class RegisterDTO
    {
        [Required, Display(Name = "Email Address", Prompt = "Email Address"), EmailAddress]
        public string Email { get; set; } = null!;

        [Required, Display(Prompt = "FirstName")]
        public string FirstName { get; set; } = null!;

        [Required, Display(Prompt = "LastName")]
        public string LastName { get; set; } = null!;


        [Required, Display(Name = "Password", Prompt = "Password"), DataType(DataType.Password)]
        public string Password { get; set; } = null!;


        [Required, Display(Name = "Confirm Password", Prompt = "Confirm Password"), DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
