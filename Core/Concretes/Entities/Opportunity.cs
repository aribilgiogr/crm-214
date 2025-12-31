using Core.Abstracts.Bases;
using Core.Concretes.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Concretes.Entities
{
    public class Opportunity : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Value { get; set; }
        public string Currency { get; set; } = null!;
        public DateTime? ExpectedCloseDate { get; set; }
        public DateTime? ActualCloseDate { get; set; }

        [ForeignKey("AssignedUser")]
        public string? AssignedUserId { get; set; }
        public virtual ApplicationUser? AssignedUser { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        public OpportunityStage Stage { get; set; } = OpportunityStage.Qualification;
        public OpportunityStatus Status { get; set; } = OpportunityStatus.Open;
    }
}
