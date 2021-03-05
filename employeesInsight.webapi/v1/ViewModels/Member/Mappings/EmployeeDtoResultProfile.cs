using AutoMapper;
using employeesInsight.data.Dtos.Member;

namespace employeesInsight.webapi.v1.ViewModels.Member.Mappings
{
    public class EmployeeDtoResultProfile : Profile
    {
        public EmployeeDtoResultProfile()
        {
            CreateMap<EmployeeDto, EmployeeDtoResult>();
        }
    }
}