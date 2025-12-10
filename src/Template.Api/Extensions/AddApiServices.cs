using Template.Api.Exceptions;

namespace Template.Api.Extensions;

/// <summary>
/// Расширения для регистрации сервисов API‑слоя в контейнере DI.
/// </summary>
public static class AddApiServices
{
    /// <summary>
    /// Добавляет в контейнер все сервисы, необходимые для работы API‑слоя:
    /// контроллеры, OpenAPI/Swagger, обработку проблем и глобальный обработчик исключений.
    /// </summary>
    /// <param name="services">Коллекция сервисов приложения.</param>
    /// <returns>Ту же коллекцию сервисов для цепочки вызовов.</returns>
    public static IServiceCollection AddApiLayer(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Info.Version = "1";
                document.Info.Title = "Template .NET 10 API";
                document.Info.Description = "Template Project";

                return Task.CompletedTask;
            });
        });

        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = ctx =>
            {
                ctx.ProblemDetails.Extensions["traceId"] = ctx.HttpContext.TraceIdentifier;
                ctx.ProblemDetails.Extensions["timestamp"] = DateTime.UtcNow;
                ctx.ProblemDetails.Instance = $"{ctx.HttpContext.Request.Method} {ctx.HttpContext.Request.Path}";
            };
        });

        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }
}
