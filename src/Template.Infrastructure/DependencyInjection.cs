using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using Template.Application.Abstractions.Persistence.Repositories;
using Template.Infrastructure.Persistence;
using Template.Infrastructure.Persistence.Options;
using Template.Infrastructure.Persistence.Repositories;

namespace Template.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        IConfiguration config)
    {
        var options = config.GetSection(nameof(PostgreSqlOptions)).Get<PostgreSqlOptions>();
        var builder = new NpgsqlConnectionStringBuilder(options!.ConnectionString)
        {
            Password = options.DbPassword
        };
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var loggerFactory = sp.GetRequiredService<ILoggerFactory>();

            options.UseNpgsql(builder.ConnectionString)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .UseLoggerFactory(loggerFactory)
                .LogTo(Serilog.Log.Logger.Information,
                       new[]
                       {
                           DbLoggerCategory.Database.Command.Name,
                           DbLoggerCategory.Query.Name,
                       },
                       LogLevel.Information,
                       DbContextLoggerOptions.SingleLine |
                       DbContextLoggerOptions.UtcTime);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILessonRepository, LessonRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

        return services;
    }
}
