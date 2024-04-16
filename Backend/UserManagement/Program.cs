using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserManagement.Interface.RepositoryInterface.CommandInterface;
using UserManagement.Interface.RepositoryInterface.QueryInterface;
using UserManagement.Interface.ServiceInterface;
using UserManagement.Interfaces.RepositoryInterfaces.QueryInterface;
using UserManagement.Interfaces.ServiceInterface;
using UserManagement.Models;
using UserManagement.Repository.CommandRepository;
using UserManagement.Repository.QueryRepository;
using UserManagement.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<HotelManagementContext>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));
builder.Services.AddScoped<ICUserManagementRepository, CUserManagementRepository>();
builder.Services.AddScoped<IQUserManagementRepository, QUserManagementRepository>();
builder.Services.AddScoped<IQRoleRepository, QRoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserManagementService, UserManagementService>();
builder.Services.AddScoped<IUserTokenService, UserTokenService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Corspolicy", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"] ?? string.Empty)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
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

app.UseCors("Corspolicy");

app.MapControllers();

app.Run();
