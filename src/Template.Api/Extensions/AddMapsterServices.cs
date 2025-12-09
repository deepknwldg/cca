using System.Reflection;
using Mapster;

namespace Template.Api.Extensions;

public static class AddMapsterServices
{
    public static IServiceCollection AddMapster(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);

        //services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}
