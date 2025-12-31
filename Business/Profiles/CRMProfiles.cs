using AutoMapper;
using Core.Concretes.DTOs;
using Core.Concretes.Entities;

namespace Business.Profiles
{
    public class CRMProfiles : Profile
    {
        public CRMProfiles()
        {
            CreateMap<Customer, CustomerListItemDTO>()
                .ForMember(dest => dest.AssignedUserName, opt => opt.MapFrom(src => src.AssignedUser != null ? $"{src.AssignedUser.FirstName} {src.AssignedUser.LastName}" : null
                ))
                .ForMember(dest => dest.ActivityCount, opt => opt.MapFrom(src => src.Activities.Count()))
                .ForMember(dest => dest.OpportunityCount, opt => opt.MapFrom(src => src.Opportunities.Count()));

            CreateMap<Lead, LeadListItemDTO>()
                .ForMember(dest => dest.AssignedUserName, opt => opt.MapFrom(src => src.AssignedUser != null ? $"{src.AssignedUser.FirstName} {src.AssignedUser.LastName}" : null
                ));

            CreateMap<LeadCreateDTO, Lead>();

            CreateMap<Activity, ActivityListItemDTO>();

            CreateMap<Lead, LeadDetailDTO>()
                 .ForMember(dest => dest.AssignedUserName, opt => opt.MapFrom(src => src.AssignedUser != null ? $"{src.AssignedUser.FirstName} {src.AssignedUser.LastName}" : null
                ));            
        }
    }
}
