using Microsoft.Extensions.Logging;
using Quartz;

namespace Template.Quartz.Jobs;

/// <summary>
/// Пример задачи - замените на вашу логику
/// </summary>
[DisallowConcurrentExecution] // Запрет параллельного выполнения
[PersistJobDataAfterExecution] // Сохранение JobDataMap после выполнения
public class SampleJob : BaseJob<SampleJob>
{
    // Inject ваши зависимости через конструктор
    // private readonly IMyService _myService;

    public SampleJob(ILogger<SampleJob> logger) : base(logger)
    {
        // _myService = myService;
    }

    protected override async Task ExecuteInternal(IJobExecutionContext context)
    {
        // Получение данных из JobDataMap если нужно
        var dataMap = context.MergedJobDataMap;

        Logger.LogError("SampleJob executing at {Time}", DateTime.UtcNow);

        // Ваша бизнес-логика здесь
        await Task.Delay(100); // Имитация работы

        Logger.LogInformation("SampleJob finished processing");
    }
}
