namespace Template.Api.Extensions;

public static class AddMapsterServices
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { }, typeof(AssemblyMarker), typeof(Application.AssemblyMarker));
        return services;
    }
}
