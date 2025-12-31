using Core.Abstracts.Bases;
using Core.Concretes.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Concretes.Entities
{
    public class Lead : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Notes { get; set; }

        [ForeignKey("ConvertedCustomer")]
        public int? ConvertedCustomerId { get; set; }
        public virtual Customer? ConvertedCustomer { get; set; }

        public DateTime? ConvertedDate { get; set; }

        [ForeignKey("AssignedUser")]
        public string? AssignedUserId { get; set; }
        public virtual ApplicationUser? AssignedUser { get; set; }

        public LeadSource Source { get; set; }
        public LeadStatus Status { get; set; } = LeadStatus.New;

        public virtual ICollection<Activity> Activities { get; set; } = [];
    }
}
