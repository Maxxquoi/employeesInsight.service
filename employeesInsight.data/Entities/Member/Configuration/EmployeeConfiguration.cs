using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace employeesInsight.data.Entities.Member.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees", "User");

            builder.HasKey(e => e.Id).HasName("PK_Employees");

            builder.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(e => e.EmployeeId).IsRequired();

            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(200).IsUnicode();

            builder.Property(e => e.LastName).IsRequired().HasMaxLength(200).IsUnicode();

            builder.Property(e => e.Email).IsRequired().HasMaxLength(200).IsUnicode();

            builder.Property(e => e.Age).IsRequired();

            builder.Property(e => e.SSN).IsRequired();

            builder.Property(e => e.BirthDate).IsRequired();

            builder.Property(e => e.Company).IsRequired().HasMaxLength(200).IsUnicode();

            builder.Property(e => e.Position).IsRequired().HasMaxLength(200).IsUnicode();

            builder.Property(e => e.Salary);

            builder.Property(e => e.Active);

            builder.HasData(
                new Employee
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

                },
                new Employee
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
                },
                new Employee
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
        }
    }
}