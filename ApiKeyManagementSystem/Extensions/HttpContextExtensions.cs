using Microsoft.AspNetCore.Mvc;

namespace ApiKeyManagementSystem.Extensions
{
    public static class HttpContextExtensions
    {
        public static Task WriteProblemAsync(this HttpContext httpContext, ProblemDetails problem, CancellationToken cancellationToken)
        {
            problem.Extensions["traceId"] = httpContext.TraceIdentifier;
            httpContext.Response.StatusCode = problem.Status ?? StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/json";
            return httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);
        }
    }
}
