using System;
using System.ComponentModel.DataAnnotations;
using employeesInsight.data.Validation;
    
namespace employeesInsight.webapi.v1.ViewModels.Member
{
    public class CreateEmployeeDto
    {
        [Required]
        [MinLength(1)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(1)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int Age { get; set; }

        [Required]
        [RegularExpression(@"^(?!000)(?!666)(?!9[0-9][0-9])\d{3}[- ]?(?!00)\d{2}[- ]?(?!0000)\d{4}$")]
        public string SSN { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [MinLength(1)]
        public string Company { get; set; }

        [Required]
        [MinLength(1)]
        public string Position { get; set; }

        [Required]
        public double Salary { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}