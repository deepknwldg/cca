using Serilog;
using Template.Api.Extensions;
using Template.Application;
using Template.Infrastructure;
using Template.Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.AddSerilog();
builder.Services.AddAutoMapper();
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddApiLayer();
builder.Services.AddQuartzScheduling(builder.Configuration);
builder.Services.AddFluentValidation();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.MapOpenApi();
app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Swagger"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.Run();
