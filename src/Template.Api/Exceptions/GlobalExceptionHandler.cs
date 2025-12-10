using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Template.Api.Exceptions;

/// <summary>
/// Глобальный обработчик исключений, который перехватывает необработанные
/// ошибки, логирует их и формирует ответ в формате <see cref="ProblemDetails"/>.
/// </summary>
/// <remarks>
/// Обработчик регистрируется в пайплайне через <c>services.AddExceptionHandler&lt;GlobalExceptionHandler&gt;()</c>.
/// </remarks>
public class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger,
    IProblemDetailsService problemDetails) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;
    private readonly IProblemDetailsService _problemDetails = problemDetails;

    /// <summary>
    /// Пытается обработать возникшее исключение, сформировать <see cref="ProblemDetails"/>
    /// и записать его в HTTP‑ответ.
    /// </summary>
    /// <param name="httpContext">Текущий <see cref="HttpContext"/> запроса.</param>
    /// <param name="exception">Исключение, которое нужно обработать.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// <c>true</c>, если исключение было обработано и дальнейшая обработка в пайплайне не требуется;
    /// иначе <c>false</c>.
    /// </returns>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        // 1) Log with correlation
        _logger.LogError(exception, "Unhandled exception. TraceId: {TraceId}", httpContext.TraceIdentifier);

        // 2) Map exception → HTTP status + title/type
        var (status, title) = exception switch
        {
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Resource Not Found"),
            ArgumentException => (StatusCodes.Status400BadRequest, "Invalid Request"),
            _ => (StatusCodes.Status500InternalServerError, "Server Error")
        };

        // 3) Build ProblemDetails (don’t leak internals in prod)
        var problem = new ProblemDetails
        {
            Status = status,
            Title = title,
            Type = exception.GetType().Name,
            Detail = httpContext.RequestServices
                                 .GetRequiredService<IHostEnvironment>()
                                 .IsDevelopment()
                     ? exception.Message
                     : null,
            Instance = httpContext.Request.Path
        };

        // 4) Enrich universally useful metadata
        problem.Extensions["traceId"] = httpContext.TraceIdentifier;
        problem.Extensions["timestamp"] = DateTime.UtcNow;

        // 5) Write response
        await _problemDetails.WriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problem,
        });

        // Tell the pipeline we handled it
        return true;
    }
}
