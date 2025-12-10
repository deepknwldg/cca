using FluentValidation;
using FluentValidation.AspNetCore;

namespace Template.Api.Extensions;

/// <summary>
/// Расширения DI‑контейнера для регистрации FluentValidation.
/// </summary>
public static class AddFluentValidatorServices
{
    /// <summary>
    /// Добавляет все валидаторы из текущей сборки и настраивает автоматическую
    /// валидацию моделей запросов.
    /// </summary>
    /// <param name="services">Коллекция сервисов приложения.</param>
    /// <returns>Ту же коллекцию сервисов для цепочки вызовов.</returns>
    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(opts =>
        {
            // Отключаем DataAnnotations, если не нужны
            opts.DisableDataAnnotationsValidation = true;
        })
        .AddFluentValidationClientsideAdapters(); // опционально

        services.AddValidatorsFromAssemblyContaining<AssemblyMarker>();

        return services;
    }
}
