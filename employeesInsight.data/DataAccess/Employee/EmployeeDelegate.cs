using System;
using AutoMapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using employeesInsight.data.Entities;
using employeesInsight.data.Entities.User;
using employeesInsight.data.Dtos.User;

namespace employeesInsight.data.DataAccess.Employee
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
                var employee = await (from e in _db.Employees where e.Id == employeeDto.Id select e).FirstOrDefaultAsync();

                if (employee == null)
                {
                    employee = new Employee
                    {
                        Id = employeeDto.Id
                    };
                }
            }
            catch (System.Exception ex)
            {
                // TODO
            }

            return null;
        }
    }
}