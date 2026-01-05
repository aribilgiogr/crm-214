using AutoMapper;
using AutoMapper.Internal.Mappers;
using Core.Abstracts;
using Core.Abstracts.IServices;
using Core.Concretes.DTOs;
using Core.Concretes.Entities;
using Core.Concretes.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Utilities.Helpers;
using Utilities.Responses;

namespace Business.Services
{
    public class LeadService : ILeadService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public LeadService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IResult> AddActivityAsync(ActivityType type, int lead_id, ClaimsPrincipal user)
        {
            try
            {
                var lead = await unitOfWork.LeadRepository.FindByIdAsync(lead_id);
                if (lead != null && user != null)
                {
                    var activity = new Activity
                    {
                        Subject = $"{type}: {lead.Name}",
                        Type = type,
                        RelatedLeadId = lead_id,
                        AssignedUserId = user.FindFirstValue(ClaimTypes.NameIdentifier),
                        DueDate = DateTime.Now,
                    };
                    await unitOfWork.ActivityRepository.CreateAsync(activity);
                    await unitOfWork.CommitAsync();
                    return new SuccessResult();
                }
                return new ErrorResult(["Lead or User not found!"]);
            }
            catch (Exception ex)
            {
                return new ErrorResult(["Operation failed!" + ex.Message]);
            }
        }

        public async Task<IResult> ConvertToCustomer(CustomerCreateDTO model)
        {
            try
            {
                var lead = await unitOfWork.LeadRepository.FindByIdAsync(model.LeadId);
                if (lead != null)
                {
                    var customer = mapper.Map<Customer>(model);
                    await unitOfWork.CustomerRepository.CreateAsync(customer);
                    await unitOfWork.CommitAsync();
                    // Bir db değişiklinde oluşan kayıttan gelecek olan Identity bilgisi commit edilmediği sürece aktarılamaz.

                    lead.Status = LeadStatus.Converted;
                    lead.UpdatedDate = DateTime.Now;
                    lead.ConvertedDate = DateTime.Now;
                    lead.ConvertedCustomerId = customer.Id;
                    await unitOfWork.LeadRepository.UpdateAsync(lead);
                    await unitOfWork.CommitAsync();
                    return new SuccessResult();
                }
                return new ErrorResult(["Lead not found!"]);
            }
            catch (Exception ex)
            {
                return new ErrorResult(["Operation failed!", ex.Message]);
            }
        }

        public async Task<IResult> CreateAsync(LeadCreateDTO model)
        {
            try
            {
                var lead = mapper.Map<Lead>(model);
                await unitOfWork.LeadRepository.CreateAsync(lead);
                await unitOfWork.CommitAsync();
                return new SuccessResult();
            }
            catch (Exception ex)
            {
                return new ErrorResult([ex.Message]);
            }
        }

        public async Task<IEnumerable<LeadListItemDTO>> GetAllAsync(ClaimsPrincipal user)
        {
            if (user.IsInRole("Admin"))
            {
                var leads = await unitOfWork.LeadRepository.FindManyAsync(null, "Activities", "ConvertedCustomer", "AssignedUser");
                return mapper.Map<IEnumerable<LeadListItemDTO>>(leads);
            }
            else
            {
                var leads = await unitOfWork.LeadRepository.FindManyAsync(x => x.AssignedUserId == user.FindFirstValue(ClaimTypes.NameIdentifier) || x.AssignedUserId == null, "Activities", "ConvertedCustomer", "AssignedUser");
                return mapper.Map<IEnumerable<LeadListItemDTO>>(leads);
            }
        }

        public async Task<LeadDetailDTO?> GetDetailAsync(int lead_id)
        {
            var leads = await unitOfWork.LeadRepository.FindManyAsync(x => x.Id == lead_id, "Activities", "AssignedUser", "ConvertedCustomer");
            var lead = leads.FirstOrDefault();
            return lead != null ? mapper.Map<LeadDetailDTO>(lead) : null;
        }

        public async Task<IResult> ImportFromFileAsync(IFormFile file)
        {
            try
            {
                using var stream = file.OpenReadStream();
                string ext = Path.GetExtension(file.FileName);

                var result = ext switch
                {
                    ".csv" => await DataImporters.ImportCsvAsync<LeadCreateDTO>(stream),
                    ".json" => await DataImporters.ImportJsonAsync<LeadCreateDTO>(stream),
                    ".xlsx" => await DataImporters.ImportExcelAsync<LeadCreateDTO>(stream),
                    _ => null
                };

                if (result == null)
                {
                    return new ErrorResult(["Only .csv, .json and .xlsx files accepted!"]);
                }

                var importedLeads = mapper.Map<IEnumerable<Lead>>(result);

                await unitOfWork.LeadRepository.CreateManyAsync(importedLeads);

                await unitOfWork.CommitAsync();

                return new SuccessResult();
            }
            catch (Exception ex)
            {
                return new ErrorResult([ex.Message]);
            }
        }

        public async Task<IResult> PickLeadAsync(int leadId, ClaimsPrincipal user)
        {
            try
            {
                var lead = await unitOfWork.LeadRepository.FindByIdAsync(leadId);
                if (lead == null)
                {
                    return new ErrorResult(["Lead not found!"]);
                }

                lead.AssignedUserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                await unitOfWork.LeadRepository.UpdateAsync(lead);
                await unitOfWork.CommitAsync();
                return new SuccessResult();
            }
            catch (Exception ex)
            {
                return new ErrorResult(["Assignment fail! " + ex.Message]);
            }
        }
    }
}
