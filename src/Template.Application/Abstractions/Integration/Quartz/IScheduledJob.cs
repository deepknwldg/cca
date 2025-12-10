using Quartz;

namespace Template.Application.Abstractions.Integration.Quartz;

/// <summary>
/// Базовый интерфейс для всех планируемых задач Quartz.
/// Наследники обязаны указать уникальное <see cref="JobName"/> и реализовать <see cref="IJob.Execute(IJobExecutionContext)"/>.
/// </summary>
public interface IScheduledJob : IJob
{
    /// <summary>
    /// Уникальное имя задачи, которое используется при регистрации в планировщике.
    /// </summary>
    static abstract string JobName { get; }
}
