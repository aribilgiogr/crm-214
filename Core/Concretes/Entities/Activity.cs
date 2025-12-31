using Core.Abstracts.Bases;
using Core.Concretes.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Concretes.Entities
{
    public class Activity : BaseEntity {
        public string Subject { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public bool IsCompleted { get; set; } = false;

        [ForeignKey("RelatedCustomer")]
        public int? RelatedCustomerId { get; set; }
        public virtual Customer? RelatedCustomer { get; set; }

        [ForeignKey("RelatedOpportunity")]
        public int? RelatedOpportunityId { get; set; }
        public virtual Opportunity? RelatedOpportunity { get; set; }

        [ForeignKey("RelatedLead")]
        public int? RelatedLeadId { get; set; }
        public virtual Lead? RelatedLead { get; set; }

        [ForeignKey("AssignedUser")]
        public string? AssignedUserId { get; set; }
        public virtual ApplicationUser? AssignedUser { get; set; }

        public ActivityType Type { get; set; }
    }
}
