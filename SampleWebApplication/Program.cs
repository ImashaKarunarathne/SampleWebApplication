using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SampleWebApplication.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SampleWebApplicationContext>(options =>
    options.UseCosmos(builder.Configuration.GetConnectionString("SampleWebApplicationContext") ?? throw new InvalidOperationException("Connection string 'SampleWebApplicationContext' not found."), "DATABASE_NAME"));

// Add services to the container.

builder.Services.AddControllers();
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
