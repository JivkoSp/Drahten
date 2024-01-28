using Drahten_Services_UserService.Data;
using Drahten_Services_UserService.Profiles;
using Microsoft.EntityFrameworkCore;
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


//Add services for Npgsql and register the dbcontext to the di container. 
builder.Services.AddDbContext<AppDbContext>(options => {

    options.UseNpgsql(builder.Configuration.GetConnectionString("UserServiceDbConnection"));
});


//Add services for AutoMapper (library for automatic mapping of objects) to the di container.
//The library defines special type: Profile, that is inherited by types, that store the object mapping configuration.
builder.Services.AddAutoMapper(configAction => {

    configAction.AddProfile<TopicProfile>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
