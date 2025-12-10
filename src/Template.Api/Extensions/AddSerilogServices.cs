using System.Reflection;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace Template.Api.Extensions;

/// <summary>
/// Расширения для настройки Serilog и отправки логов в Elasticsearch.
/// </summary>
public static class AddSerilogServices
{
    /// <summary>
    /// Конфигурирует глобальный <see cref="Log.Logger"/> и подключает Serilog к хосту.
    /// </summary>
    /// <param name="builder">Объект <see cref="WebApplicationBuilder"/> текущего приложения.</param>
    public static void AddSerilog(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentName()
            .Enrich.WithExceptionDetails()
            .WriteTo.Elasticsearch(ConfigureElasticSink(builder.Configuration))
            .CreateLogger();

        builder.Host.UseSerilog();
    }

    /// <summary>
    /// Формирует параметры <see cref="ElasticsearchSinkOptions"/> из конфигурации
    /// (раздел <c>ElasticConfiguration</c>).
    /// </summary>
    /// <param name="configuration">Корневой объект <see cref="IConfiguration"/>.</param>
    /// <returns>Настроенный объект <see cref="ElasticsearchSinkOptions"/>.</returns>
    static ElasticsearchSinkOptions ConfigureElasticSink(IConfiguration configuration)
    {
        var elasticConfiguration = configuration.GetSection("ElasticConfiguration");
        var uri = elasticConfiguration.GetValue<Uri>("Uri");
        var branch = elasticConfiguration.GetValue<string>("Branch");
        var indexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-")}-{branch}";

        return new ElasticsearchSinkOptions(uri)
        {
            AutoRegisterTemplate = true,
            IndexFormat = indexFormat
        };
    }
}
