using Drahten_ApiGateway_Yarp.Logging.Formatters;
using Drahten_ApiGateway_Yarp.Middlewares;
using Drahten_ApiGateway_Yarp.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;

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

var rateLimitOptions = builder.Configuration.GetSection("RateLimitOptions").Get<RateLimitOptions>();

builder.Services.AddSingleton(rateLimitOptions);

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
            if(authFailedContext.HttpContext.Request != null)
            {
                Log.Error(authFailedContext.Exception, "Yarp Gateway --> Jwt token validation failed.");
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

// Add services and configuration for Yarp reverse proxy.
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("Yarp"));


var app = builder.Build();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<RateLimitMiddleware>(rateLimitOptions);

app.MapReverseProxy();

app.Run();
