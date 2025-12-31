using Core.Concretes.DTOs;
using System.Security.Claims;

namespace Core.Abstracts.IServices
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerListItemDTO>> GetAllAsync(ClaimsPrincipal user);
        Task<CustomerDetailDTO> GetAsync(int id);
    }
}
