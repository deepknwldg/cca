using Microsoft.Extensions.DependencyInjection;
using Template.Application.Abstractions.Services;
using Template.Application.Services;

namespace Template.Application;

/// <summary>
/// Расширения DI‑контейнера, регистрирующие все сервисы уровня
/// Application‑слоя. Вызывается из `Program.cs` через
/// <c>services.AddApplicationLayer()</c>.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавляет в <see cref="IServiceCollection"/> реализации всех
    /// бизнес‑сервисов приложения.
    /// </summary>
    /// <param name="services">Коллекция сервисов проекта.</param>
    /// <returns>Ту же коллекцию для цепочки вызовов.</returns>
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ILessonService, LessonService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IEnrollmentService, EnrollmentService>();

        return services;
    }
}
