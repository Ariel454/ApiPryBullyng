using ApiPryBullyng.Models.DB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// DATABASE CONNECTION
builder.Configuration.AddJsonFile("appsettings.json");
string? connectionString = builder.Configuration.GetConnectionString("MyDataBase");

// Manejar el caso de valor nulo
if (connectionString is null)
{
    throw new InvalidOperationException("The connection string 'MyDataBase' was not found.");
}

builder.Services.AddDbContext<AppbullyingContext>(options =>
  options.UseSqlServer(connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
