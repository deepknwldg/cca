namespace Template.Api.Extensions;

/// <summary>
/// Расширения DI‑контейнера для конфигурации AutoMapper.
/// </summary>
public static class AddAutoMapperServices
{
    /// <summary>
    /// Регистрирует профили AutoMapper из текущей и из слоя Application.
    /// </summary>
    /// <param name="services">Коллекция сервисов приложения.</param>
    /// <returns>Ту же коллекцию сервисов для цепочки вызовов.</returns>
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { }, typeof(AssemblyMarker), typeof(Application.AssemblyMarker));
        return services;
    }
}
