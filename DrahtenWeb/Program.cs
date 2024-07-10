using DrahtenWeb.Automapper.Profiles;
using DrahtenWeb.Logging.Formatters;
using DrahtenWeb.Services;
using DrahtenWeb.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
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

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IPrivateHistoryService, PrivateHistoryService>();

builder.Services.AddHttpClient<IPublicHistoryService, PublicHistoryService>();

builder.Services.AddHttpClient<ITopicArticleService, TopicArticleService>();

builder.Services.AddHttpClient<IUserService, UserService>();

builder.Services.AddHttpClient<ISearchService, SearchService>();

builder.Services.AddScoped<IPrivateHistoryService, PrivateHistoryService>();

builder.Services.AddScoped<IPublicHistoryService, PublicHistoryService>();

builder.Services.AddScoped<ITopicArticleService, TopicArticleService>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ISearchService, SearchService>();

builder.Services.AddAutoMapper(configAction => {

    configAction.AddProfile<ArticleProfile>();
});

builder.Services.AddMemoryCache();

builder.Services.AddAuthentication(options => {

    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
}).AddCookie(options => {

    options.Events.OnSigningIn = context => {

        var ipAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();
        var userName = context.Principal?.Identity?.Name;

        Log.Information($"DrahtenWeb --> User signing In: {userName} from IP: {ipAddress} at {DateTimeOffset.Now.ToUniversalTime()}");

        return Task.CompletedTask;
    };

    options.Events.OnSigningOut = context =>
    {
        var ipAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();
        var userName = context.HttpContext.User?.Identity?.Name;

        Log.Information($"DrahtenWeb --> User signing Out: {userName} from IP: {ipAddress} at {DateTimeOffset.Now.ToUniversalTime()}");

        return Task.CompletedTask;
    };

}).AddOpenIdConnect(options => {

    options.ClientId = "drahten-client";
    options.Authority = "http://127.0.0.1:8080/realms/drahten";
    options.ClientSecret = "AD8iuDSIhjSA2d3TU94T5Imt6WWFMz7c";
    options.ResponseType = OpenIdConnectResponseType.Code;
    options.SaveTokens = true;
    options.Scope.Add("openid");
    options.RequireHttpsMetadata = false;
    options.UsePkce = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = "http://127.0.0.1:8080/realms/drahten",
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        NameClaimType = "name",
        RoleClaimType = "role"
    };

    options.ClaimActions.MapAllExcept("iss", "nbf", "exp", "aud", "nonce", "iat", "c_hash");
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
