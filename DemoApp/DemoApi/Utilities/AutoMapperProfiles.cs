using DemoApi.Models;
using Domain.DTO;
using Domain.Entities;

namespace DemoApi.Utilities
{
    public class AutoMapperProfiles : AutoMapper.Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<EmployeeModel, EmployeeDTO>()
                .ForMember(a => a.Id,
                    opt => opt.MapFrom(src => Guid.Parse(src.Id))
                )
                .ForMember(e => e.Company,
                    opt => opt.MapFrom(src => src.Company));
                

            CreateMap<Employee, EmployeeModel>()
                .ForMember(a => a.Id,
                    opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<EmployeeModel, EmployeeDTO>();
            CreateMap<EmployeeDTO, EmployeeModel>();

            CreateMap<Company, CompanyModel>();
            CreateMap<CompanyModel, Company>();
        }
    }
}
