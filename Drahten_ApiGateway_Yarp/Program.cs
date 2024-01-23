var builder = WebApplication.CreateBuilder(args);

// Add services and configuration for the Yarp reverse proxy.
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("Yarp"));


var app = builder.Build();

app.MapReverseProxy();

app.Run();
