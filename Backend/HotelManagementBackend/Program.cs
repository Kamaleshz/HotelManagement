using AutoMapper;
using HotelManagementBackend.Interfaces.RepositoryInterface.QueryInterfaces;
using HotelManagementBackend.Interfaces.ServiceInterfaces;
using HotelManagementBackend.Middlewares;
using HotelManagementBackend.Models;
using HotelManagementBackend.Repositories.QueryRepos;
using HotelManagementBackend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRoomServices, RoomService>();
builder.Services.AddScoped<IRoomQuery, RoomQuery>();
builder.Services.AddTransient<GlobalExceptionMiddleware>();


var connectionString = builder.Configuration.GetConnectionString("SQLConnection");

builder.Services.AddDbContext<HotelManagementContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("CORS", options =>
    {
        options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CORS");

app.UseMiddleware<GlobalExceptionMiddleware>();


app.MapControllers();

app.Run();
