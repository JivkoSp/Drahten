using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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
                Console.WriteLine("Jwt token validation failed from UserService.");
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

app.UseMiddleware<UserRegistrationMiddleware>();

app.MapControllers();

app.Run();

public partial class Program { }
