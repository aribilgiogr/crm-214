using Core.Concretes.Enums;

namespace Core.Concretes.DTOs
{
    public class CustomerDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? TaxNumber { get; set; }
        public string? Industury { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public bool IsPerson { get; set; }
        public CustomerStatus Status { get; set; }
        public string? AssignedUserId { get; set; }
        public string? AssignedUserName { get; set; }
        public IEnumerable<ContactDTO> Contacts { get; set; } = [];
        public IEnumerable<ActivityListItemDTO> Activities { get; set; } = [];
        public IEnumerable<OpportunityListItemDTO>  Opportunities { get; set; } = [];
    }
}
