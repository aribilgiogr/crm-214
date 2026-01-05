using Core.Concretes.DTOs;
using Core.Concretes.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Utilities.Responses;

namespace Core.Abstracts.IServices
{
    public interface ILeadService
    {
        Task<IEnumerable<LeadListItemDTO>> GetAllAsync(ClaimsPrincipal user);
        Task<IResult> CreateAsync(LeadCreateDTO model);
        Task<IResult> ImportFromFileAsync(IFormFile file);
        Task<IResult> PickLeadAsync(int leadId, ClaimsPrincipal user);

        Task<IResult> AddActivityAsync(ActivityType type, int lead_id, ClaimsPrincipal user);

        Task<LeadDetailDTO?> GetDetailAsync(int lead_id);
        Task<IResult> ConvertToCustomer(CustomerCreateDTO model);
    }
}
