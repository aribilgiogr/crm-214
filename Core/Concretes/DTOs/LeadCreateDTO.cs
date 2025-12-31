using Core.Concretes.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.Concretes.DTOs
{
    public class LeadCreateDTO
    {
        [Required, Display(Prompt = "Name")]
        public string Name { get; set; } = null!;

        [Required, EmailAddress, Display(Name = "Email Address", Prompt = "Email Address")]
        public string Email { get; set; } = null!;

        [Display(Name = "Phone Number", Prompt = "Phone Number"), DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        [Display(Prompt = "Notes"), DataType(DataType.MultilineText)]
        public string? Notes { get; set; }

        [Required, Display(Prompt = "Source")]
        public LeadSource Source { get; set; }
    }
}
