using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using PrivateHistoryService.Application.Extensions;
using PrivateHistoryService.Infrastructure.Extensions;
using PrivateHistoryService.Infrastructure.Logging.Formatters;
using PrivateHistoryService.Presentation.Middlewares;
using Serilog;
using System.Threading.RateLimiting;

// Add Serilog configuration
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console() // Add console sink.
    .WriteTo.Http(requestUri: "http://localhost:5000",
                  queueLimitBytes: 1000000,
                  batchFormatter: new SerilogJsonFormatter()) //Add http sink.
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog();

builder.Services.AddSingleton(serviceProvider =>
{
    var options = new TokenBucketRateLimiterOptions
    {
        TokenLimit = 100, // Max number of tokens in the bucket
        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
        QueueLimit = 10, // Allows for brief traffic bursts
        ReplenishmentPeriod = TimeSpan.FromSeconds(1),
        TokensPerPeriod = 10, // Tokens added per replenishment period
        AutoReplenishment = true
    };

    return new TokenBucketRateLimiter(options);
});


builder.Services.AddControllers();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddTransient<ErrorHandlerMiddleware>();

builder.Services.AddTransient<RateLimitingMiddleware>();

builder.Services.AddTransient<UserRegistrationMiddleware>();

builder.Services.AddAuthentication(options => {

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {

    options.Authority = "http://127.0.0.1:8080/realms/drahten";
    // *** IMPORTANT ***
    //This is for development ONLY. The reason is that the OpenID provider uses http.
    //In production this should be removed, becouse the OpenID provider must use https.
    options.RequireHttpsMetadata = false;
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = authFailedContext =>
        {
            if (authFailedContext.HttpContext.Request != null)
            {
                Console.WriteLine("Jwt token validation failed from PrivateHistoryService.");
                //TODO: Log information, about this event to logging service.
            }

            return Task.CompletedTask;
        }
    };
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // *** IMPORTANT ***
        //For development ONLY
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization();

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

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseMiddleware<RateLimitingMiddleware>();

app.UseMiddleware<UserRegistrationMiddleware>();

app.MapControllers();

app.Run();

public partial class Program { }