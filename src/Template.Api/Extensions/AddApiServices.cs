using Template.Api.Exceptions;

namespace Template.Api.Extensions;

public static class AddApiServices
{
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
