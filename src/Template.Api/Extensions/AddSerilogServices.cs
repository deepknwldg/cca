using System.Reflection;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace Template.Api.Extensions;

public static class AddSerilogServices
{
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
