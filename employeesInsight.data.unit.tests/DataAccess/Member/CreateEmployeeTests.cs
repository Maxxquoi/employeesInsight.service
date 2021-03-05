using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using FluentAssertions;
using employeesInsight.data.DataAccess.Member;
using employeesInsight.data.Entities;
using employeesInsight.data.Dtos.Member;

namespace employeesInsight.data.unit.tests.DataAccess.Member
{
    [TestClass]
    public class CreateEmployeeTests
    {
        private static IMapper Mapper { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context) => Mapper = EmployeesMapperProvider.CreateAutoMapper();

        [TestMethod]
        public async Task CreateEmployee_Successful()
        {
            //Setup
            await using var db = EmployeesDbTestProvider.CreateInMemoryEmployeeDb();
            var employeeDelegate = GetEmployeeDelegate(db);

            var employeeDto = new EmployeeDto
            {
                EmployeeId = Guid.Parse("51b3cf08-3c5b-4e7c-a441-6809177f44e2"),
                FirstName = "Joel",
                LastName = "Salsbury",
                Email = "JoelSalsbury@here.com",
                Age = 21,
                SSN = "464-79-9012",
                BirthDate = new DateTime(200, 1, 4),
                Company = "Sagebrush",
                Position = "Nursery Worker",
                Salary = 70000,
                Active = true
            };

            //SUT
            var newEmployee = await employeeDelegate.CreateEmployeeAsync(employeeDto);

            //Assertions
            newEmployee.EmployeeId.Should().Be("51b3cf08-3c5b-4e7c-a441-6809177f44e2");
            newEmployee.FirstName.Should().Be("Joel");
            newEmployee.LastName.Should().Be("Salsbury");
            newEmployee.Email.Should().Be("JoelSalsbury@here.com");
            newEmployee.Age.Should().Be(21);
            newEmployee.SSN.Should().Be("464-79-9012");
            newEmployee.BirthDate.Should().Be(new DateTime(200, 1, 4));
            newEmployee.Company.Should().Be("Sagebrush");
            newEmployee.Position.Should().Be("Nursery Worker");
            newEmployee.Salary.Should().Be(70000);
            newEmployee.Active.Should().Be(true);
            db.Employees.Should().HaveCount(4);
        }

        private EmployeeDelegate GetEmployeeDelegate(EmployeesDb db)
        {
            return new EmployeeDelegate(Mapper, db);
        }
    }
}
