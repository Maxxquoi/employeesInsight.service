using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;


namespace employeesInsight.webapi.Filters
{
    public class ExceptionFilterBaseAttribute : ExceptionFilterAttribute
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ExceptionFilterBaseAttribute> _logger;
        private readonly Dictionary<Type, HttpStatusCode> _desiredStatusCodes;
        private readonly IWebHostEnvironment _environment;

        public ExceptionFilterBaseAttribute(ILogger<ExceptionFilterBaseAttribute> logger, IConfiguration configuration, Dictionary<Type, HttpStatusCode> desiredStatusCodes, IWebHostEnvironment environment)
        {
            _logger = logger;
            _configuration = configuration;
            _desiredStatusCodes = desiredStatusCodes;
            _environment = environment;
        }

        public override void OnException(ExceptionContext context)
        {
            var disableGlobalExceptionFilter = _configuration["DisableGlobalExceptionFilter"] == "true";

            if(context != null && !disableGlobalExceptionFilter)
            {
                CommonHandler(context);
            }

            base.OnException(context);
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var disableGlobalExceptionFilter = _configuration["DisableGlobalExceptionFilter"] == "true";

            if(context != null && !disableGlobalExceptionFilter)
            {
                CommonHandler(context);
            }

            return base.OnExceptionAsync(context);
        }

        private void CommonHandler(ExceptionContext context)
        {
            var exception = context.Exception;
            var request = context.HttpContext.Request;
            var baseUrl = $"{request?.Scheme}://{request?.Host}{request?.PathBase}{request?.Path}?{request?.QueryString}";

            var isDomainSpecificException = _desiredStatusCodes.TryGetValue(exception.GetType(), out var suggestedStatusCode);

            object errorResponse = new 
            {
                error = new
                {
                    code = exception.GetType().Name,
                    message = _environment.IsProduction() ? exception.Message : exception.ToString()
                }
            };

            var statusCode = context.HttpContext.Response.StatusCode;

            if (isDomainSpecificException)
            {
                statusCode = (int)suggestedStatusCode;

                _logger.LogDebug(exception, $"A domain specific exception was raised while calling {baseUrl}");
                
            } else
            {
                statusCode = 500;
#if !DEBUG
                errorResponse = new 
                {
                    error = new 
                    {
                        code = "UnhandleException",
                        message = "An unexpected error occurred."
                    }
                };
#else
                errorResponse = new
                {
                    error = new
                    {
                        code = "UnhandledException",
                        message = context.Exception.GetBaseException().Message
                    }
                };
#endif
                _logger.LogError(exception, $"An unexpected exception was raised while calling {baseUrl}");
            }

            context.Result = new JsonResult(errorResponse);

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = statusCode;
        }
    }
}