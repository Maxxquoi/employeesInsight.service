using System.Threading.Tasks;
using AutoMapper;
using employeesInsight.data.DataAccess.Member;
using employeesInsight.data.Dtos.Member;
using employeesInsight.webapi.v1.Filters.Member;
using employeesInsight.webapi.v1.ViewModels.Member;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace employeesInsight.webapi.v1.Controllers.Member
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    [TypeFilter(typeof(EmployeeExceptionFilterAttribute))]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger _log;
        private readonly IMapper _mapper;
        private readonly EmployeeDelegate _employeeDelegate;
        
        public EmployeesController(EmployeeDelegate employeeDelegate, ILogger log, IMapper mapper)
        {
            _employeeDelegate = employeeDelegate;
            _mapper = mapper;
            _log = log;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto employee)
        {
            var createEmployee = await _employeeDelegate.CreateEmployeeAsync(_mapper.Map<EmployeeDto>(employee));
            _log.LogInformation($"Employee {employee.Email} was created successfully");

            return new ObjectResult(_mapper.Map<EmployeeDtoResult>(createEmployee)) {StatusCode = 201};
        }
    }
}
