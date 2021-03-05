using AutoMapper;
using employeesInsight.data.Dtos.Member;

namespace employeesInsight.webapi.v1.ViewModels.Member.Mappings
{
    public class CreateEmployeeProfile : Profile
    {
        public CreateEmployeeProfile()
        {
            CreateMap<CreateEmployeeDto, EmployeeDto>();
        }
    }
}