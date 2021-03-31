using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OpenReferrals.RegisterManagementConnector.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("/error-local-development")]
        public IActionResult ErrorLocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var error = context.Error;
            if (context.Error is AggregateException aggregateException)
            {
                var baseException = aggregateException.GetBaseException();
                error = baseException;
            }

            if (error is RegisterClientException siccarException)
            {
                return Problem(
                    title: siccarException.Message,
                    statusCode: (int)siccarException.Status,
                    detail: error.StackTrace
                    );
            }

            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message);
        }

        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var error = context.Error;
            if (context.Error is AggregateException aggregateException)
            {
                var baseException = aggregateException.GetBaseException();
                error = baseException;
            }

            if (error is RegisterClientException siccarException)
            {
                return Problem(
                    title: siccarException.Message,
                    statusCode: (int)siccarException.Status
                    );
            }

            return Problem();
        }

    }
}
