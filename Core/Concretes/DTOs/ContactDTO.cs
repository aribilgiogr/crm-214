namespace Core.Concretes.DTOs
{
    public class ContactDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Title { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? MobilePhone { get; set; }
        public bool IsPrimary { get; set; }
    }
}
