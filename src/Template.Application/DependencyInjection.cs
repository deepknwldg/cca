using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Template.Application.Abstractions.Services;
using Template.Application.Services;

namespace Template.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IServiceExecutor, ServiceExecutor>();
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ILessonService, LessonService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IEnrollmentService, EnrollmentService>();

        return services;
    }
}
