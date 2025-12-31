using AutoMapper;
using Core.Abstracts;
using Core.Abstracts.IServices;
using Core.Concretes.DTOs;
using System.Security.Claims;

namespace Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CustomerListItemDTO>> GetAllAsync(ClaimsPrincipal user)
        {
            if (user.IsInRole("Admin"))
            {
                var customers = await unitOfWork.CustomerRepository.FindManyAsync(null, "Activities", "Opportunities", "AssignedUser");
                return mapper.Map<IEnumerable<CustomerListItemDTO>>(customers);
                /*
                return from c in customers
                       select new CustomerListItemDTO
                       {
                           Id = c.Id,
                           Name = c.Name,
                           AssignedUserId = c.AssignedUserId,
                           AssignedUserName = c.AssignedUserId != null ? (c.AssignedUser.FirstName + " " + c.AssignedUser.LastName) : null,
                           ActivityCount = c.Activities.Count(),
                           OpportunityCount = c.Opportunities.Count(),
                           IsPerson = c.IsPerson,
                           Status = c.Status
                       };
                */
            }
            else
            {
                var customers = await unitOfWork.CustomerRepository.FindManyAsync(x => x.AssignedUserId == user.FindFirstValue(ClaimTypes.NameIdentifier), "Activities", "Opportunities", "AssignedUser");
                return mapper.Map<IEnumerable<CustomerListItemDTO>>(customers);
            }
        }

        public Task<CustomerDetailDTO> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}