using Core.Concretes.Enums;

namespace Core.Concretes.DTOs
{
    public class CustomerListItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsPerson { get; set; }
        public CustomerStatus Status { get; set; }
        public string? AssignedUserId { get; set; }
        public string? AssignedUserName { get; set; }
        public int ActivityCount { get; set; }
        public int OpportunityCount { get; set; }
    }
}
