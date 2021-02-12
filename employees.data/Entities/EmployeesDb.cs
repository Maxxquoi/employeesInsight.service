using Microsoft.EntityFrameworkCore;
using employees.data.Entities.User;
using employees.data.Entities.User.Configuration;

namespace employees.data.Entities
{
    public class EmployeesDb : DbContext
    {
        public EmployeesDb()
        {

        }

        public EmployeesDb(DbContextOptions<EmployeesDb> options) : base(options)
        {

        }

        public virtual DbSet<Employee> Employees { get; set; }

        private static void ConfigureUserSchema(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureUserSchema(modelBuilder);
        }
    }
}