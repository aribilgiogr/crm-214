using Core.Abstracts;
using Core.Abstracts.IRepositories;
using Data.Contexts;
using Data.Repositories;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext context;

        public UnitOfWork(ApplicationContext context)
        {
            this.context = context;
        }

        private ICustomerRepository? customerRepository;
        public ICustomerRepository CustomerRepository => customerRepository ??= new CustomerRepository(context);

        /*
        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (customerRepository == null)
                {
                   customerRepository = new CustomerRepository(context);
                }
                return customerRepository;
            }
        }
        */

        // LazyLoading: Buradaki nesnelerin gerekmediği sürece üretilmemesini/yani sadece çağırıldıklarında (get) yoksa oluşturulmalarını varsa var olanı kullanmayı sağlar. Singleton (tekil) üretim olarak çalışır.

        private IActivityRepository? activityRepository;
        public IActivityRepository ActivityRepository => activityRepository ??= new ActivityRepository(context);

        private IContactRepository? contactRepository;
        public IContactRepository ContactRepository => contactRepository ??= new ContactRepository(context);

        private ILeadRepository? leadRepository;
        public ILeadRepository LeadRepository => leadRepository ??= new LeadRepository(context);

        private IOpportunityRepository? opportunityRepository;
        public IOpportunityRepository OpportunityRepository => opportunityRepository ??= new OpportunityRepository(context);

        public async Task CommitAsync()
        {
            try
            {
                // Veri tabanına işlemleri gönderir.
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // İşlemler sırasında herhangi bir hata oluşursa UnitOfWork bellek temizleme metoduna yönlenir.
                await DisposeAsync();

                // Her türlü hata mesajı verilecektir.
                throw ex;
            }
        }

        public async ValueTask DisposeAsync()
        {
            // UnitOfWork bellekten temizlenirken (hata veya bilinçli) veritabanı bilgileri de bellekten silinir.
            await context.DisposeAsync();
        }
    }
}
