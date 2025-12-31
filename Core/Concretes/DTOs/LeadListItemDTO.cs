using Core.Concretes.Enums;

namespace Core.Concretes.DTOs
{
    public class LeadListItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Notes { get; set; }
        public int? ConvertedCustomerId { get; set; }
        public DateTime? ConvertedDate { get; set; }
        public string? AssignedUserId { get; set; }
        public string? AssignedUserName { get; set; }
        public LeadSource Source { get; set; }
        public LeadStatus Status { get; set; }
    }
}
