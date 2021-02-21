using System;
using System.Collections.Generic;
using System.Net;
using employeesInsight.webapi.Filters;
using Microsoft.Extensions.Logging;
using employeesInsight.data.Exceptions.Member;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace employeesInsight.webapi.v1.Filters.Member
{
    public class EmployeeExceptionFilterAttribute : ExceptionFilterBaseAttribute
    {
        private static readonly Dictionary<Type, HttpStatusCode> StatusCodeMapping = new Dictionary<Type, HttpStatusCode>
        {
            [typeof(EmployeeNotFoundException)] = HttpStatusCode.NotFound
        };
        
        public EmployeeExceptionFilterAttribute(ILogger<EmployeeExceptionFilterAttribute> logger, IConfiguration configuration, IWebHostEnvironment enviroment) : base(logger, configuration, StatusCodeMapping, enviroment)
        {
            
        }
    }

}