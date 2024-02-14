using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shared.Core;

namespace BookStore.Api.ExceptionHandlers
{
    public class DomainExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<DomainExceptionHandler> _logger;

        public DomainExceptionHandler(ILogger<DomainExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if(exception is DomainException ex)
            {
                _logger.LogWarning(ex, "Domain exception occured: {code}, {}", ex.Code, ex.Message);
                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    Title = ex.Code,
                    Status = (int)StatusCodes.Status400BadRequest,
                    Detail = exception.Message,
                    Extensions = new Dictionary<string, object?>
                    {
                        ["traceId"] = httpContext.TraceIdentifier
                    }
                };
                httpContext.Response.StatusCode = (int)StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(problemDetails);
                return true;
            }

            return false;
        }
    }
}
