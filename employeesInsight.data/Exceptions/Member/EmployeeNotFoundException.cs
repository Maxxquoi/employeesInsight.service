using System;
using System.Runtime.Serialization;

namespace employeesInsight.data.Exceptions.Member
{
    [Serializable]
    public class EmployeeNotFoundException : Exception
    {
        private const string StandardErrorMessage = "Unable to locate the employee [{0}]";

        public string EmployeeId { get; set; }

        public EmployeeNotFoundException(string employeeId) : base(string.Format(StandardErrorMessage, employeeId))
        {
            EmployeeId = employeeId;
        }

        public EmployeeNotFoundException(string employeeId, Exception innerException) : base(string.Format(StandardErrorMessage, employeeId), innerException)
        {
            EmployeeId = employeeId;
        }

        protected EmployeeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}