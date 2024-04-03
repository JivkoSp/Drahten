using TopicArticleService.Application.Extensions;
using TopicArticleService.Infrastructure.Extensions;
using TopicArticleService.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add logging configuration
builder.Logging.ClearProviders(); // Clear default logging providers

builder.Logging.AddConsole(); // Add Console logger

builder.Logging.SetMinimumLevel(LogLevel.Debug); // Set minimum log level to Debug

builder.Services.AddControllers();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddTransient<ErrorHandlerMiddleware>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
