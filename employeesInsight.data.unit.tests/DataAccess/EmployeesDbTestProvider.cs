using System;
using Microsoft.EntityFrameworkCore;
using employeesInsight.data.Entities;
using employeesInsight.data.Entities.User;

namespace employeesInsight.data.unit.tests.DataAccess
{
    public class EmployeesDbTestProvider
    {
        public static EmployeesDb CreateInMemoryEmployeeDb()
        {
            var options = new DbContextOptionsBuilder<EmployeesDb>()
                .UseInMemoryDatabase(databaseName: $"InMemory:{DateTime.Now.GetHashCode()}{DateTime.UtcNow.Millisecond}")
                .Options;

            var db = new EmployeesDb(options);

            db.Employees.Add(new Employee
            {
                Id = 100,
                EmployeeId = new Guid("066f2deb-d25d-44e6-9116-bac34e0b58fe"),
                FirstName = "Larry",
                LastName = "Bailey",
                Email = "LarryBailey@here.com",
                Age = 65,
                SSN = "451-21-0000",
                BirthDate = new DateTime(1956, 1, 3),
                Company = "National Tea",
                Position = "Keeper",
                Salary = 75000.50,
                Active = false

            });

            db.Employees.Add(new Employee
            {
                Id = 103,
                EmployeeId = new Guid("77751f1a-0bcc-4187-9c24-f0658e2e899f"),
                FirstName = "Mary",
                LastName = "Leeper",
                Email = "MaryLeeper@here.com",
                Age = 29,
                SSN = "388-25-1111",
                BirthDate = new DateTime(1991, 8, 6),
                Company = "Millenia Life",
                Position = "Recruitment Manager",
                Salary = 100000,
                Active = true
            });

            db.Employees.Add(new Employee
            {
                Id = 106,
                EmployeeId = new Guid("6251a09c-3d49-44b8-80f8-681365ad6173"),
                FirstName = "George",
                LastName = "Lockwood",
                Email = "GeorgeLockwood@here.com",
                Age = 39,
                SSN = "017-56-2222",
                BirthDate = new DateTime(1981, 9, 21),
                Company = "Specialty Restaurant Group",
                Position = "Limnologist",
                Salary = 70000,
                Active = true
            }
            );

            db.SaveChanges();
            return db;
        }
    }
}