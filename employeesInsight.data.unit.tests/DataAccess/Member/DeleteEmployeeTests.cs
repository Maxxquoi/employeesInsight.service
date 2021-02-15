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
    public class DeletEmployeeTests
    {
        private static IMapper Mapper { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context) => Mapper = EmployeesMapperProvider.CreateAutoMapper();

        [TestMethod]
        public async Task DeleteEmployee_Successful()
        {
            //Setup
            await using var db = EmployeesDbTestProvider.CreateInMemoryEmployeeDb();
            var employeeDelegate = GetEmployeeDelegate(db);

            var employeeId = Guid.Parse("066f2deb-d25d-44e6-9116-bac34e0b58fe");

            //SUT
            var deletedEmployee = await employeeDelegate.DeleteEmployeeAsync(employeeId);
            Console.WriteLine(deletedEmployee);

            //Assertions
            deletedEmployee.EmployeeId.Should().Be("066f2deb-d25d-44e6-9116-bac34e0b58fe");
            deletedEmployee.FirstName.Should().Be("Larry");
            deletedEmployee.LastName.Should().Be("Bailey");
            deletedEmployee.Email.Should().Be("LarryBailey@here.com");
            deletedEmployee.Age.Should().Be(65);
            deletedEmployee.SSN.Should().Be("451-21-0000");
            deletedEmployee.BirthDate.Should().Be(new DateTime(1956, 1, 3));
            deletedEmployee.Company.Should().Be("National Tea");
            deletedEmployee.Position.Should().Be("Keeper");
            deletedEmployee.Salary.Should().Be(75000.50);
            deletedEmployee.Active.Should().Be(false);
            db.Employees.Should().HaveCount(2);
        }

        [TestMethod]
        public async Task DeleteEmployee_Unsucessfull()
        {
            //Setup
            await using var db = EmployeesDbTestProvider.CreateInMemoryEmployeeDb();
            var employeeDelegate = GetEmployeeDelegate(db);

            var employeeId = Guid.Parse("066f2deb-d25d-44e6-9116-bac34e0b58ff");

            //SUT
            Func<Task> act = async () => { await employeeDelegate.DeleteEmployeeAsync(employeeId); };

            //Assertions
            await act.Should().ThrowAsync<EmployeeNotFoundException>().WithMessage($"Unable to locate the employee [{employeeId.ToString()}]");
        }

        private static EmployeeDelegate GetEmployeeDelegate(EmployeesDb db) => new EmployeeDelegate(Mapper, db);
    }
}