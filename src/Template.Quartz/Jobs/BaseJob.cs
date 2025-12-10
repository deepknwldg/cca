using Microsoft.Extensions.Logging;
using Quartz;

namespace Template.Quartz.Jobs;

/// <summary>
/// Базовый класс для всех задач с логированием и обработкой ошибок
/// </summary>
public abstract class BaseJob<TJob> : IJob where TJob : class
{
    protected readonly ILogger<TJob> Logger;

    protected BaseJob(ILogger<TJob> logger)
    {
        Logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var jobName = typeof(TJob).Name;
        var fireInstanceId = context.FireInstanceId;

        Logger.LogInformation(
            "Job {JobName} started. FireInstanceId: {FireInstanceId}",
            jobName,
            fireInstanceId);

        try
        {
            await ExecuteInternal(context);

            Logger.LogInformation(
                "Job {JobName} completed successfully. Duration: {Duration}ms",
                jobName,
                context.JobRunTime.TotalMilliseconds);
        }
        catch (Exception ex)
        {
            Logger.LogError(
                ex,
                "Job {JobName} failed. FireInstanceId: {FireInstanceId}",
                jobName,
                fireInstanceId);

            // Опционально: можно настроить retry-логику
            throw new JobExecutionException(ex, refireImmediately: false);
        }
    }

    /// <summary>
    /// Основная логика задачи - реализуется в наследниках
    /// </summary>
    protected abstract Task ExecuteInternal(IJobExecutionContext context);
}
