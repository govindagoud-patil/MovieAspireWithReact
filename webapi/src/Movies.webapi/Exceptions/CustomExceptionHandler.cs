using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Movies.Contracts.Exceptions;

namespace Movies.webapi.Exceptions
{
    /// <summary>
    /// Exception Handler
    /// </summary>
    public class CustomExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = CreateProblemDetails(exception);
            httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }

        private static ProblemDetails CreateProblemDetails(Exception exception)
        {
            ProblemDetails problemDetails = exception switch
            {
                NotFoundException => CreateProblemDetails(StatusCodes.Status404NotFound, "Not Found", exception.Message),
                CustomValidationException => CreateProblemDetails(StatusCodes.Status400BadRequest, "validation error", "One or more validation error occured"),
                _ => CreateProblemDetails(StatusCodes.Status500InternalServerError, "Internal Server Exception ", exception.Message)

            };

            if (exception is CustomValidationException customValidationException)
            {
                problemDetails.Extensions["errors"] = customValidationException.Errors;
            }

            return problemDetails;
        }

        private static ProblemDetails CreateProblemDetails(int status, string title, string details)
        {
            return new ProblemDetails()
            {
                Status = status,
                Title = title,
                Detail = details
            };
        }
    }
}
