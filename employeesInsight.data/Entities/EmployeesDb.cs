using Microsoft.EntityFrameworkCore;
using employeesInsight.data.Entities.User;
using employeesInsight.data.Entities.User.Configuration;

namespace employeesInsight.data.Entities
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