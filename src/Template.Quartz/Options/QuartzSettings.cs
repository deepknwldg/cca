namespace Template.Quartz.Options;

/// <summary>
/// Корневой объект конфигурации Quartz, который привязывается к секции
/// <c>Quartz</c> в <c>appsettings.json</c> (или в других провайдерах
/// конфигурации). Содержит набор расписаний задач, где каждый элемент
/// – это отдельный <see cref="JobScheduleSettings"/>.
/// </summary>
public class QuartzSettings
{
    /// <summary>
    /// Имя секции конфигурации, используемое при привязке:
    /// <c>builder.Configuration.GetSection(QuartzSettings.SectionName)</c>.
    /// </summary>
    public const string SectionName = "Quartz";

    /// <summary>
    /// Словарь, в котором ключом является уникальное имя задачи,
    /// а значением – параметры её расписания (<see cref="JobScheduleSettings"/>).
    /// </summary>
    public Dictionary<string, JobScheduleSettings> Jobs { get; set; } = new();
}
