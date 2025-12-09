namespace Template.Api.Extensions;

public static class AddApiServices
{
    public static IServiceCollection AddApiLayer(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        return services;
    }
}
