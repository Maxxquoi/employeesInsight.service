using System;

namespace employeesInsight.webapi.v1.ViewModels.Member
{
    public class EmployeeDtoResult
    {
    
        public Guid EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public string SSN { get; set; }

        public DateTime BirthDate { get; set; }

        public string Company { get; set; }

        public string Position { get; set; }

        public double Salary { get; set; }

        public bool Active { get; set; }
    }
}