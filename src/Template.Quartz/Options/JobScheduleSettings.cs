namespace Template.Quartz.Options;


/// <summary>
/// Настройки расписания для отдельной задачи
/// </summary>
public class JobScheduleSettings
{
    /// <summary>
    /// Включена ли задача
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Cron-выражение расписания
    /// </summary>
    public string CronExpression { get; set; } = string.Empty;

    /// <summary>
    /// Описание задачи
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Таймзона (по умолчанию UTC)
    /// </summary>
    public string TimeZone { get; set; } = "UTC";
}
