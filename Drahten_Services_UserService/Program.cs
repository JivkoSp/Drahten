using Drahten_Services_UserService.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Add services for Npgsql and register the dbcontext to the di container. 
builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<AppDbContext>(options => {

        options.UseNpgsql(builder.Configuration.GetConnectionString("UserServiceDbConnection"));
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
