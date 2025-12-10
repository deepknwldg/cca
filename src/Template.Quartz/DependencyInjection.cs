using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Template.Quartz.Jobs;
using Template.Quartz.Options;

namespace Template.Quartz;

/// <summary>
/// Методы расширения <see cref="IServiceCollection"/> для регистрации Quartz.NET
/// и привязки его параметров из конфигурационного файла (<c>appsettings.json</c>,
/// переменных окружения и т.п.).
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавляет в контейнер DI все необходимые сервисы Quartz, привязывает
    /// настройки из секции <c>Quartz</c> конфигурации и регистрирует все задачи,
    /// указанные в <see cref="QuartzSettings.Jobs"/>.
    /// </summary>
    /// <param name="services">
    /// Коллекция сервисов приложения, в которую будут добавлены Quartz‑сервисы.
    /// </param>
    /// <param name="configuration">
    /// Объект <see cref="IConfiguration"/>, из которого извлекаются настройки
    /// Quartz (секция <c>Quartz</c>).
    /// </param>
    /// <returns>
    /// Ту же коллекцию <see cref="IServiceCollection"/> для поддержки
    /// цепочки вызовов.
    /// </returns>
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

    // <summary>
    /// Регистрация отдельной задачи (<see cref="IJob"/>) в Quartz с использованием
    /// параметров, заданных в <see cref="QuartzSettings"/>.
    /// </summary>
    /// <typeparam name="TJob">
    /// Тип задачи, реализующий <see cref="IJob"/> и имеющий публичный конструктор без параметров.
    /// </typeparam>
    /// <param name="quartz">
    /// Конфигуратор Quartz, получаемый из <c>services.AddQuartz(...)</c>.
    /// </param>
    /// <param name="settings">
    /// Объект <see cref="QuartzSettings"/> с набором конфигураций всех задач.
    /// </param>
    /// <param name="jobConfigKey">
    /// Ключ, под которым в словаре <see cref="QuartzSettings.Jobs"/>
    /// хранится конфигурация конкретной задачи.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Выбрасывается, если в конфигурации найден ключ задачи, но для него не указано
    /// обязательное поле <c>CronExpression</c>.
    /// </exception>
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
