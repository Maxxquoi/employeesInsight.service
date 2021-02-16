using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using FluentAssertions;
using employeesInsight.data.unit.tests.DataAccess;
using employeesInsight.data.DataAccess.Member;
using employeesInsight.data.Entities;
using employeesInsight.data.Dtos.Member;
using employeesInsight.data.Exceptions.Member;

namespace employeesInsight.data.unit.DataAccess.Member
{
    [TestClass]
    public class UpdateEmployeeTests
    {
        private static IMapper Mapper { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context) => Mapper = EmployeesMapperProvider.CreateAutoMapper();

        [TestMethod]
        public async Task UpdateEmployee_EmployeeExist()
        {
            //Setup
            await using var db = EmployeesDbTestProvider.CreateInMemoryEmployeeDb();
            var employeeDelegate = GetEmployeeDelegate(db);

            var employeeDto = new EmployeeDto
            {
                EmployeeId = Guid.Parse("066f2deb-d25d-44e6-9116-bac34e0b58fe"),
                FirstName = "Jack",
                LastName = "Zero",
                Email = "JackZero@here.com",
                Age = 30,
                SSN = "908-34-7033",
                BirthDate = new DateTime(1990, 1, 4)
            };

            //SUT
            var newEmployee = await employeeDelegate.UpdateEmployeeAsync(employeeDto);

            //Assertions
            newEmployee.EmployeeId.Should().Be("066f2deb-d25d-44e6-9116-bac34e0b58fe");
            newEmployee.FirstName.Should().Be("Jack");
            newEmployee.LastName.Should().Be("Zero");
            newEmployee.Email.Should().Be("JackZero@here.com");
            newEmployee.Age.Should().Be(30);
            newEmployee.SSN.Should().Be("908-34-7033");
            newEmployee.BirthDate.Should().Be(new DateTime(1990, 1, 4));
            db.Employees.Should().HaveCount(3);
        }

        [TestMethod]
        public async Task UpdateEmployee_EmployeeDoesNotExist()
        {
            //Setup
            await using var db = EmployeesDbTestProvider.CreateInMemoryEmployeeDb();
            var employeeDelegate = GetEmployeeDelegate(db);

            var employeeDto = new EmployeeDto
            {
                EmployeeId = Guid.Parse("066f2deb-d25d-44e6-9116-bac34e0b58ff"),
                FirstName = "Jack",
                LastName = "Zero",
                Email = "JackZero@here.com",
                Age = 30,
                SSN = "908-34-7033",
                BirthDate = new DateTime(1990, 1, 4)
            };

            //SUT
            Func<Task> act = async () => { await employeeDelegate.UpdateEmployeeAsync(employeeDto); };

            //Assertions
            await act.Should().ThrowAsync<EmployeeNotFoundException>().WithMessage($"Unable to locate the employee [{employeeDto.EmployeeId}]");
        }

        private EmployeeDelegate GetEmployeeDelegate(EmployeesDb db)
        {
            return new EmployeeDelegate(Mapper, db);
        }
    }
}
