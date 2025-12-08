using Mapster;
using System.Reflection;

namespace Template.Api.Extensions;

public static class AddApiServices
{
    public static IServiceCollection AddApiLayer(this IServiceCollection services)
    {
        services.AddControllers();

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly()); // find ApiMappingConfig

        services.AddSingleton(config);
        //services.AddScoped<IMapper, ServiceMapper>();

        services.AddEndpointsApiExplorer();

        return services;
    }
}
