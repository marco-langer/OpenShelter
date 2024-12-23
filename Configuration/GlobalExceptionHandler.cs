using Microsoft.AspNetCore.Diagnostics;
using OpenShelter.Exceptions;

namespace OpenShelter.Configuration;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        (Int32 statusCode, String? detail) = MapException(exception);

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(exception, "Unable to handle request on {MachineName}", Environment.MachineName);
        }

        await Results.Problem(statusCode: statusCode, detail: detail).ExecuteAsync(httpContext);

        return true;
    }

    private (Int32, String?) MapException(Exception exception)
    {
        return exception switch
        {
            NotFoundException ex => (StatusCodes.Status404NotFound, ex.Message),
            ConflictException ex => (StatusCodes.Status409Conflict, ex.Message),
            NotImplementedException => (StatusCodes.Status501NotImplemented, default),
            _ => (StatusCodes.Status500InternalServerError, default)
        };
    }
}
