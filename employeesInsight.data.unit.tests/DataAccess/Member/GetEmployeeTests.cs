using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using FluentAssertions;
using employeesInsight.data.Entities;
using employeesInsight.data.DataAccess.Member;
using employeesInsight.data.Exceptions.Member;

namespace employeesInsight.data.unit.tests.DataAccess.Member
{
    [TestClass]
    public class GetEmployeeTests
    {
        private static IMapper Mapper { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context) => Mapper = EmployeesMapperProvider.CreateAutoMapper();

        [TestMethod]
        public async Task GetEmployee_EmployeeExist()
        {
            //Setup
            await using var db = EmployeesDbTestProvider.CreateInMemoryEmployeeDb();
            var employeeDelegate = GetEmployeeDelegate(db);

            var employeeId = Guid.Parse("066f2deb-d25d-44e6-9116-bac34e0b58fe");

            //SUT
            var employee = await employeeDelegate.GetEmployeeAsync(employeeId);

            //Assertions
            employee.EmployeeId.Should().Be(employeeId);
        }

        [TestMethod]
        public async Task GetEmployee_EmployeeDoesNotExist()
        {
            //Setup
            await using var db = EmployeesDbTestProvider.CreateInMemoryEmployeeDb();
            var employeeDelegate = GetEmployeeDelegate(db);

            var employeeId = Guid.Parse("066f2deb-d25d-44e6-9116-bac34e0b58ff");

            //SUT
            Func<Task> act = async () => { await employeeDelegate.GetEmployeeAsync(employeeId); };

            //Assertions
            await act.Should().ThrowAsync<EmployeeNotFoundException>().WithMessage($"Unable to locate the employee [{employeeId.ToString()}]");
        }

        private EmployeeDelegate GetEmployeeDelegate(EmployeesDb db)
        {
            return new EmployeeDelegate(Mapper, db);
        }
    }
}