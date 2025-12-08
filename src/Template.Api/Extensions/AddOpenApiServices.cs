namespace Template.Api.Extensions;

public static class AddOpenApiServices
{
    public static IServiceCollection AddOpenApiLayer(this IServiceCollection services)
    {
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

        return services;
    }
}
