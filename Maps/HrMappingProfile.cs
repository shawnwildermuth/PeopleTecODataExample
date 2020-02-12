using AutoMapper;
using Persontec.Api.Data.Entities;
using Persontec.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persontec.Api.Maps
{
  public class HrMappingProfile : Profile
  {
    public HrMappingProfile()
    {
      CreateMap<Employee, EmployeeViewModel>()
        .ForMember(c => c.OrganizationName, 
          o => o.MapFrom(s => s.Organization.OrganizationName))
        .ForMember(c => c.OrganizationCode,
          o => o.MapFrom(s => s.Organization.OrganizationCode))
        .ReverseMap()
        .ForMember(c => c.Organization, o => o.Ignore());
    }
  }
}
