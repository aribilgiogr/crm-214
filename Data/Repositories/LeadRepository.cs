using Core.Abstracts.IRepositories;
using Core.Concretes.Entities;
using Data.Contexts;
using Utilities.Generics;

namespace Data.Repositories
{
    public class LeadRepository : Repository<Lead>, ILeadRepository
    {
        public LeadRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
