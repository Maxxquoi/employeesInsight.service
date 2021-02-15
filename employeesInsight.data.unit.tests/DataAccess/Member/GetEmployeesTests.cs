using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using FluentAssertions;
using employeesInsight.data.Entities;
using employeesInsight.data.DataAccess.Member;

namespace employeesInsight.data.unit.tests.DataAccess.Member
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
            var employeeDelegate = GetEmployeeDelegate(db);

            //SUT
            var employees = await employeeDelegate.GetEmployeesAsync();

            //Assertions
            employees.employees.Should().HaveCount(3);
            employees.available.Should().Be(3);
        }

        private EmployeeDelegate GetEmployeeDelegate(EmployeesDb db)
        {
            return new EmployeeDelegate(Mapper, db);
        }
    }

}