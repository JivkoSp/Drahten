using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

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
                Console.WriteLine("Jwt token validation failed from Yarp Gateway.");
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

// Add services and configuration for Yarp reverse proxy.
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("Yarp"));


var app = builder.Build();

app.UseAuthentication();

app.UseAuthorization();

app.MapReverseProxy();

app.Run();
