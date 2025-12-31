using Business.Profiles;
using Business.Services;
using Core.Abstracts;
using Core.Abstracts.IServices;
using Core.Concretes.Entities;
using Data;
using Data.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business
{
    // IOC (Inversion of Control) Sınıfı: Yapıcı metotları kullanarak içeri almaya çalıştığımız bağımlılıkları buraya tanımlıyoruz. Bu sayede new operatörü kullanmaya gerek kalmadan oturum boyunca çalışacak, bellek taşımına (stack overflow) veya sızıntısına (memory leak) izin vermeyen bir düzen oluşur. apıcı metotlarda yazılan yapıya Dependency Inversion, burada yazılan karşılığına ise Dependency Injection denir.
    public static class IOC
    {
        // IServiceCollection: net core tarafında bağımlılıkların (Dependencies) tutulduğu koleksiyondur.
        // IConfiguration: net core tarafında bulunana "appsettings.json" dosya içeriğine nesnel olarak erişen yapıdır.
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Veri tabanı ile ilgili bağımlılık (Context Dependency) buraya eklenir. (eskiden new ApplicationContext())
            services.AddDbContext<ApplicationContext>(options => options.UseSqlite(configuration.GetConnectionString("app_db")));

            // Kimlik yönetimi için veri tabanı tanımlamasından sonra Identity kütüphanesi eklenir.
            services.AddIdentity<ApplicationUser, ApplicationUserRole>()
                    .AddEntityFrameworkStores<ApplicationContext>()
                    .AddDefaultTokenProviders();

            // AutoMapper Sınıf Tipi Dönüştürücü.
            services.AddAutoMapper(config =>
            {
                config.AddProfile(typeof(CRMProfiles));
            });

            // Kimlik yönetimi servisi:
            services.AddScoped<IAuthService, AuthService>();

            // Uygulama verileri yönetimi:
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ILeadService, LeadService>();

            return services;
        }
    }
}
