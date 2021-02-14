using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using employeesInsight.data.Entities;
using employeesInsight.data.Entities.Member;
using employeesInsight.data.Dtos.Member;
using employeesInsight.data.Exceptions;
using employeesInsight.data.Exceptions.Member;

namespace employeesInsight.data.DataAccess.Member
{
    public class EmployeeDelegate
    {
        private readonly IMapper _mapper;
        private readonly EmployeesDb _db;

        public EmployeeDelegate(IMapper mapper, EmployeesDb db)
        {
            _mapper = mapper;
            _db = db;
        }

        public virtual async Task<EmployeeDto> CreateEmployeeAsync(EmployeeDto employeeDto)
        {
            try
            {
                var employee = await (from e in _db.Employees where e.EmployeeId == employeeDto.EmployeeId select e).FirstOrDefaultAsync();

                if (employee == null)
                {
                    employee = new Employee
                    {
                        EmployeeId = employeeDto.EmployeeId,
                        FirstName = employeeDto.FirstName,
                        LastName = employeeDto.LastName,
                        Email = employeeDto.Email,
                        Age = employeeDto.Age,
                        SSN = employeeDto.SSN,
                        BirthDate = employeeDto.BirthDate,
                        Company = employeeDto.Company,
                        Position = employeeDto.Position,
                        Salary = employeeDto.Salary,
                        Active = employeeDto.Active
                    };

                    _db.Employees.Add(employee);
                }
                else
                {
                    employee.FirstName = employeeDto.FirstName;
                    employee.LastName = employeeDto.LastName;
                    employee.Email = employeeDto.Email;
                    employee.Age = employeeDto.Age;
                    employee.SSN = employeeDto.SSN;
                    employee.BirthDate = employeeDto.BirthDate;
                    employee.Company = employeeDto.Company;
                    employee.Position = employeeDto.Position;
                    employee.Salary = employeeDto.Salary;
                    employee.Active = employeeDto.Active;

                    _db.Entry(employee).State = EntityState.Modified;
                }

                await _db.SaveChangesAsync();

                return _mapper.Map<EmployeeDto>(employee);
            }
            catch (Exception e) when (e is DbUpdateException || e is DbUpdateException)
            {
                throw new PersistenceException($"An unexpected error occurred whill persisting the employee: ({JsonSerializer.Serialize(employeeDto)})", e);
            }
        }

        public virtual async Task<EmployeeDto> GetEmployeeAsync(Guid employeeId)
        {
            var employee = await (from e in _db.Employees
                                  where e.EmployeeId == employeeId
                                  select e).FirstOrDefaultAsync();

            if (employee == null)
            {
                throw new EmployeeNotFoundException(employeeId.ToString());
            }

            var filteredEntity = new Employee
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Age = employee.Age,
                SSN = employee.SSN,
                BirthDate = employee.BirthDate,
                Company = employee.Company,
                Position = employee.Position,
                Salary = employee.Salary,
                Active = employee.Active
            };

            return _mapper.Map<EmployeeDto>(filteredEntity);
        }
    }
}
