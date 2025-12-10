using FluentValidation;
using FluentValidation.AspNetCore;

namespace Template.Api.Extensions;

public static class AddFluentValidatorServices
{
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
