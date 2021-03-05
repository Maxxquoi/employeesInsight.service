using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using employeesInsight.data.DataAccess.Member;
using employeesInsight.data.Dtos.Member;
using employeesInsight.webapi.v1.Controllers.Member;
using employeesInsight.webapi.v1.ViewModels.Member;
using Serilog.Core;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace employeesInsight.webapi.unit.tests.v1.Controllers.Member
{
    [TestClass]
    public class GetEmployeesTests
    {
        private static IMapper Mapper { get; set; }
        
        [ClassInitialize]
        public static void ClassInitialize(TestContext context) => Mapper = EmployeesMapperProvider.CreateAutoMapper();
        
        [TestMethod]
        public async Task GetEmployees_ReturnAllEmployeesSuccessful()
        {
            //Setup
            await using var db = EmployeesDbTestProvider.CreateInMemoryEmployeeDb();
            var employeeDelegate = new Mock<EmployeeDelegate>(Mapper, db);
            var employeeDto = new EmployeeDto()
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
            employeeDelegate.Setup(f => f.GetEmployeesAsync().Result).Returns((new List<EmployeeDto>{ employeeDto }, 1));
            
            var employeeController = new EmployeesController(employeeDelegate.Object, Logger.None, Mapper);

            //SUT
            var getEmployees = await employeeController.GetEmployees();

            //Assertions
            var result = getEmployees as ObjectResult;
            var employeeDtoList = result?.Value as List<EmployeeDtoResult> ?? new List<EmployeeDtoResult>();

            employeeDtoList.Should().HaveCount(1);
            db.Employees.Should().HaveCount(3);

        }
    }
}