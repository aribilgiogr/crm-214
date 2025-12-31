using Core.Concretes.Enums;

namespace Core.Concretes.DTOs
{
    public class OpportunityListItemDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal Value { get; set; }
        public string Currency { get; set; } = null!;
        public DateTime? ExpectedCloseDate { get; set; }
        public DateTime? ActualCloseDate { get; set; }
        public OpportunityStage Stage { get; set; }
        public OpportunityStatus Status { get; set; }
    }
}
