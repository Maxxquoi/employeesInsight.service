using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using Moq;
using employeesInsight.data.DataAccess.Member;
using employeesInsight.data.Dtos.Member;
using employeesInsight.webapi.v1.Controllers.Member;
using employeesInsight.webapi.v1.ViewModels.Member;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Core;

namespace employeesInsight.webapi.unit.tests.v1.Controllers.Member
{
    [TestClass]
    public class CreateEmpolyeeTests
    {
        private static IMapper Mapper { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context) => Mapper = EmployeesMapperProvider.CreateAutoMapper();
        
        [TestMethod]
        public async Task CreateEmpolyee_Successful()
        {
            //Setup
            await using var db = EmployeesDbTestProvider.CreateInMemoryEmployeeDb();
            var employeeDelegate = new Mock<EmployeeDelegate>(Mapper, db);
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
            employeeDelegate.Setup(f => f.CreateEmployeeAsync(It.IsAny<EmployeeDto>()).Result).Returns(employeeDto);
            var employeeController = new EmployeesController(employeeDelegate.Object, Logger.None, Mapper);

            //SUT
            var createEmployee = await employeeController.CreateEmployee(new CreateEmployeeDto());
            var result = createEmployee as ObjectResult;
            var employeeDtoResult = result?.Value as EmployeeDtoResult ?? new EmployeeDtoResult();
            
            //Assertions
            employeeDtoResult.EmployeeId.Should().Be(employeeDto.EmployeeId);
            employeeDtoResult.FirstName.Should().Be(employeeDto.FirstName);
            employeeDtoResult.LastName.Should().Be(employeeDto.LastName);
            employeeDtoResult.Email.Should().Be(employeeDto.Email);
            employeeDtoResult.Age.Should().Be(employeeDto.Age);
            employeeDtoResult.SSN.Should().Be(employeeDto.SSN);
            employeeDtoResult.BirthDate.Should().Be(employeeDto.BirthDate);
            employeeDtoResult.Company.Should().Be(employeeDto.Company);
            employeeDtoResult.Position.Should().Be(employeeDto.Position);
            employeeDtoResult.Salary.Should().Be(employeeDto.Salary);
            employeeDtoResult.Active.Should().Be(employeeDto.Active);
            db.Employees.Should().HaveCount(3);
        }
    }
}
