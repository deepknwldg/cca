namespace Template.Quartz.Options;

/// <summary>
/// Корневые настройки Quartz из appsettings
/// </summary>
public class QuartzSettings
{
    public const string SectionName = "Quartz";

    public Dictionary<string, JobScheduleSettings> Jobs { get; set; } = new();
}
