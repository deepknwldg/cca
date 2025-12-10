using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Template.Quartz.Jobs;
using Template.Quartz.Options;

namespace Template.Quartz;

public static class DependencyInjection
{
    /// <summary>
    /// Регистрация Quartz с конфигурацией из appsettings
    /// </summary>
    public static IServiceCollection AddQuartzScheduling(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Привязка настроек
        var quartzSettings = new QuartzSettings();
        configuration.GetSection(QuartzSettings.SectionName).Bind(quartzSettings);
        services.Configure<QuartzSettings>(configuration.GetSection(QuartzSettings.SectionName));

        // Регистрация Quartz
        services.AddQuartz(quartz =>
        {
            // Регистрация всех задач
            RegisterJob<SampleJob>(quartz, quartzSettings, "SampleJob");
        });

        // Hosted service для запуска Quartz
        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        return services;
    }

    /// <summary>
    /// Регистрация отдельной задачи с конфигурацией
    /// </summary>
    private static void RegisterJob<TJob>(
        IServiceCollectionQuartzConfigurator quartz,
        QuartzSettings settings,
        string jobConfigKey) where TJob : class, IJob
    {
        // Проверяем наличие конфигурации для данной задачи
        if (!settings.Jobs.TryGetValue(jobConfigKey, out var jobSettings))
        {
            return; // Задача не сконфигурирована - пропускаем
        }

        if (!jobSettings.Enabled)
        {
            return; // Задача отключена
        }

        if (string.IsNullOrWhiteSpace(jobSettings.CronExpression))
        {
            throw new InvalidOperationException(
                $"CronExpression is required for job '{jobConfigKey}'");
        }

        var jobKey = new JobKey(jobConfigKey);

        // Регистрация job
        quartz.AddJob<TJob>(opts => opts
            .WithIdentity(jobKey)
            .WithDescription(jobSettings.Description ?? $"{typeof(TJob).Name} job")
            .StoreDurably());

        // Регистрация trigger с cron-расписанием
        quartz.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity(jobConfigKey)
            .WithDescription($"Trigger for {jobConfigKey}")
            .WithCronSchedule(
                jobSettings.CronExpression,
                cronBuilder => cronBuilder.WithMisfireHandlingInstructionFireAndProceed()));
    }
}
