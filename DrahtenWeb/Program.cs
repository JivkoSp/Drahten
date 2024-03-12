using DrahtenWeb.Middlewares;
using DrahtenWeb.Services;
using DrahtenWeb.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IUserService, UserService>();

builder.Services.AddHttpClient<ISearchService, SearchService>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ISearchService, SearchService>();

builder.Services.AddAuthentication(options => {

    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
}).AddCookie(options => {

    options.Events.OnSigningIn = context => {

        //TODO: Log information, about this event to logging service.

        return Task.CompletedTask;
    };

}).AddOpenIdConnect(options => {

    options.ClientId = "drahten-client";
    options.Authority = "http://127.0.0.1:8080/realms/drahten";
    options.ClientSecret = "8L2FkUCnCgc5940DarL4CrApmQXv4jCc";
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
app.UseMiddleware<SynchronizeUser>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
