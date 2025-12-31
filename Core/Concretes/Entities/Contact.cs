using Core.Abstracts.Bases;

namespace Core.Concretes.Entities
{
    public class Contact : BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Title { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? MobilePhone { get; set; }
        public bool IsPrimary { get; set; } = false;

        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
    }
}
