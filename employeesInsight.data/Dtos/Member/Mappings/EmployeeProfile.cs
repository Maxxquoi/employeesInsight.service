using System;
using AutoMapper;
using employeesInsight.data.Entities.Member;

namespace employeesInsight.data.Dtos.Member.Mappings
{
    public class EmployeeProfile : Profile
    {
        private static Guid GetEmployeeId(Employee entity) => entity?.EmployeeId ?? Guid.Empty;
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(viewModel => viewModel.EmployeeId, o => o.MapFrom(entity => GetEmployeeId(entity)))
                .ForMember(viewModel => viewModel.FirstName, o => o.MapFrom(entity => entity.FirstName))
                .ForMember(viewModel => viewModel.LastName, o => o.MapFrom(entity => entity.LastName))
                .ForMember(viewModel => viewModel.Email, o => o.MapFrom(entity => entity.Email))
                .ForMember(viewModel => viewModel.Age, o => o.MapFrom(entity => entity.Age))
                .ForMember(viewModel => viewModel.SSN, o => o.MapFrom(entity => entity.SSN))
                .ForMember(viewModel => viewModel.BirthDate, o => o.MapFrom(entity => entity.BirthDate))
                .ForMember(viewModel => viewModel.Company, o => o.MapFrom(entity => entity.Company))
                .ForMember(viewModel => viewModel.Position, o => o.MapFrom(entity => entity.Position))
                .ForMember(viewModel => viewModel.Salary, o => o.MapFrom(entity => entity.Salary))
                .ForMember(viewModel => viewModel.Active, o => o.MapFrom(entity => entity.Active));
        }
    }
}