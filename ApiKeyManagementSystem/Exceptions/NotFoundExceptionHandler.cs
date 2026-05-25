using ApiKeyManagementSystem.Extensions;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ApiKeyManagementSystem.Exceptions
{
    public class NotFoundExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<NotFoundExceptionHandler> _logger;

        public NotFoundExceptionHandler(ILogger<NotFoundExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not NotFoundException notFound) return false;
            _logger.LogWarning("Recurso no encontrado: Tipo: {ResourceName}, Id: {ResourceId}", notFound.ResourceName, notFound.ResourceId );
            
            var problem = new ProblemDetails
            {
                Title = "El recurso solicitado no fue encontrado.",
                Status = StatusCodes.Status404NotFound,
                Type = "https://tools.ietf.org/html/rfc7807#section-3",
            };

            await httpContext.WriteProblemAsync(problem, cancellationToken);

            return true;
        }
    }
}
