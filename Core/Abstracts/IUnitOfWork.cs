using Core.Abstracts.IRepositories;

namespace Core.Abstracts
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ICustomerRepository CustomerRepository { get; }
        IActivityRepository ActivityRepository { get; }
        IContactRepository ContactRepository { get; }
        ILeadRepository LeadRepository { get; }
        IOpportunityRepository OpportunityRepository { get; }

        Task CommitAsync();
    }
}
