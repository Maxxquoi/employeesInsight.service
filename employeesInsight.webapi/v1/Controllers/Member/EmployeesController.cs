using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using employeesInsight.data.DataAccess.Member;
using employeesInsight.data.Dtos.Member;
using employeesInsight.webapi.v1.Filters.Member;
using employeesInsight.webapi.v1.ViewModels.Member;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

namespace employeesInsight.webapi.v1.Controllers.Member
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
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
        [SwaggerResponse(StatusCodes.Status201Created, "Employee created successfully", typeof(EmployeeDtoResult))]
        [SwaggerOperation(
            Summary = "Creates a new employee",
            Description = "Creates a new employee",
            OperationId = "CreateEmployee",
            Tags = new[] {"Employee"}
            )]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto employee)
        {
            var createEmployee = await _employeeDelegate.CreateEmployeeAsync(_mapper.Map<EmployeeDto>(employee));
            _log.Information($"Employee {employee.Email} was created successfully");

            return new ObjectResult(_mapper.Map<EmployeeDtoResult>(createEmployee)) {StatusCode = 201};
        }

        [HttpGet]
        [Route("")]
        [SwaggerResponse(StatusCodes.Status200OK, "Employee list retrieved successfully", typeof(List<EmployeeDtoResult>))]
        [SwaggerOperation(Summary = "Gets a list of employees", Description = "Gets list of employees", OperationId = "GetEmployees", Tags = new[] {"Employees"})]
        public async Task<IActionResult> GetEmployees()
        {
            _log.Information("Getting all empolyees");
            var (retrievedEmployees, available) = await _employeeDelegate.GetEmployeesAsync();

            return Ok(_mapper.Map<List<EmployeeDtoResult>>(retrievedEmployees));
        }
    }
}
