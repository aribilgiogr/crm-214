using Core.Concretes.Enums;

namespace Core.Concretes.DTOs
{
    public class CustomerCreateDTO
    {
        public string Name { get; set; } = null!;
        public string? TaxNumber { get; set; }
        public string? Industury { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? AssignedUserId { get; set; }
        public bool IsPerson { get; set; }
        public CustomerStatus Status { get; set; }
        public int LeadId { get; set; }
    }
}
