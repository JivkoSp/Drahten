using Drahten_Services_UserService.Data;
using Drahten_Services_UserService.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
//Configuring that the JsonSerializer will create round-trippable JSON from objects,
//that contain cycles or duplicate references.
//IMPORTANT: If this configuration is omitted, the application will throw:
//System.Text.Json.JsonException: A possible object cycle was detected. This can either be due to a cycle or
//if the object depth is larger than the maximum allowed depth of 32.
.AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);


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


//Add services for Npgsql and register the dbcontext to the di container. 
builder.Services.AddDbContext<AppDbContext>(options => {

    options.UseNpgsql(builder.Configuration.GetConnectionString("UserServiceDbConnection"));
});


//Add services for AutoMapper (library for automatic mapping of objects) to the di container.
//The library defines special type: Profile, that is inherited by types, that store the object mapping configuration.
builder.Services.AddAutoMapper(configAction => {

    configAction.AddProfile<TopicProfile>();
    configAction.AddProfile<UserProfile>();
    configAction.AddProfile<UserTopicProfile>();
    configAction.AddProfile<ArticleProfile>();
    configAction.AddProfile<UserArticleProfile>();
    configAction.AddProfile<ArticleLikeProfile>();
    configAction.AddProfile<ArticleCommentProfile>();
    configAction.AddProfile<ArticleCommentThumbsUpProfile>();
    configAction.AddProfile<ArticleCommentThumbsDownProfile>();
});

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

app.MapControllers();

app.Run();
