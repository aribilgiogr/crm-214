using Core.Abstracts.IRepositories;
using Core.Concretes.Entities;
using Data.Contexts;
using Utilities.Generics;

namespace Data.Repositories
{
    public class OpportunityRepository : Repository<Opportunity>, IOpportunityRepository
    {
        public OpportunityRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
