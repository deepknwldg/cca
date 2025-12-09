using Template.Api.Exceptions;
using Template.Api.Extensions;
using Template.Application;
using Template.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApiLayer();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApiLayer();
builder.Services.AddProblemDetailsLayer();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

app.MapOpenApi();
app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Swagger"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.Run();
