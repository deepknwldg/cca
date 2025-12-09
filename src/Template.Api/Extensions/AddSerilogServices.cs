using Serilog;

namespace Template.Api.Extensions;

public static class AddSerilogServices
{
    public static void AddSerilog(this WebApplicationBuilder builder)
    {
        builder.Configuration
            .AddJsonFile("serilog.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"serilog.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

        builder.Host.UseSerilog((context, services, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName);
        });
    }
}
