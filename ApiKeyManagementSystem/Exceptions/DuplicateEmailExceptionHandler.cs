using ApiKeyManagementSystem.Extensions;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ApiKeyManagementSystem.Exceptions
{
    public class DuplicateEmailExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<DuplicateEmailExceptionHandler> _logger;

        public DuplicateEmailExceptionHandler(ILogger<DuplicateEmailExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not DuplicateEmailException duplicateEmail) return false;
            _logger.LogWarning("Email duplicado: {Email}",  duplicateEmail.Email);

            var problem = new ProblemDetails
            {
                Title = "El recurso existe.",
                Status = StatusCodes.Status409Conflict,
                Type = "https://tools.ietf.org/html/rfc7807#section-3",
            };

            await httpContext.WriteProblemAsync(problem, cancellationToken);

            return true;
        }
    }
}
