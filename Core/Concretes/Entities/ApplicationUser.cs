using Microsoft.AspNetCore.Identity;

namespace Core.Concretes.Entities
{
    // Identity  teknolojisi kullanılacaksa kullanıcı yapısı da bu kütüphaneden sağlanır, ek özellikler içinse kalıtım alınır.

    // IdentityUser<string>: Kullanmak istenilen primary key veri tipi jenerik olarak yazılır, güvenlik için 'string' tavsiye edilir.
    public class ApplicationUser : IdentityUser
    {
        // IdentityUser'dan gelen özellikler.
        // Id (string), UserName, Email, EmailConfirmed, PasswordHash, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount

        public string FirstName { get; set; } = null!; // null!: Bu alanın (string) boş olmayacağını garanti ettiğimizi belirtir.
        public string LastName { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastLoginDate { get; set; } // ?: Bu alanın boş olabileceğini belirtir.
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Activity> Activities { get; set; } = [];
        public virtual ICollection<Opportunity> Opportunities { get; set; } = [];
        public virtual ICollection<Customer> Customers { get; set; } = [];
        public virtual ICollection<Lead> Leads { get; set; } = [];
    }
}
