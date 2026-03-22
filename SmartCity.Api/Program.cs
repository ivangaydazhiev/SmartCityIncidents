using Microsoft.EntityFrameworkCore;
using FluentValidation;
using SmartCity.Application.Interfaces;
using SmartCity.Application.Services;
using SmartCity.Application.Validators;
using SmartCity.Infrastructure.Persistence;
using SmartCity.Infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using SmartCity.Api.Middlewares;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();
builder.Services.AddScoped<IIncidentService, IncidentService>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateIncidentDtoValidator>();
builder.Services.AddDbContext<SmartCityDbContext>(options =>
    options.UseSqlite("Data source=smartcity.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
