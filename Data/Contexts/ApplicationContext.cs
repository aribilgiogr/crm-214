using Core.Concretes.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts
{
    // DbContext: Veri tabanı bağlantısı sağlayarak tablo yönetimini gerçekleştirir.
    // IdentityDbContext: Veri tabanı bağlantısını Kullanıcı desteği (Identity) ile sağlayarak tablo yönetimini gerçekleştirir.
    public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationUserRole, string>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Lead> Leads { get; set; }
        public virtual DbSet<Opportunity> Opportunities { get; set; }
    }
}
