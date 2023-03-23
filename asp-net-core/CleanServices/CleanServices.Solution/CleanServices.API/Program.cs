using AutoMapper;
using CleanServices.API.Infrastructure.Exceptions.StatusCode;
using CleanServices.API.Infrastructure.Extensions;
using CleanServices.API.Infrastructure.Services.Clients;
using CleanServices.API.Infrastructure.Services.Hashers;
using CleanServices.Data.Infrastructure.Contexts;
using CleanServices.Mappers;
using CleanServices.Validators.Models.Client;
using CleanServices.Validators.Models.Credentials.Password;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Controllers

builder.Services.AddControllers()
    .UseCustomInvalidModelStateResponse();

#endregion

#region Mapper

builder.Services.AddAutoMapper(opt =>
{
    var profiles = typeof(ClientProfile).Assembly.GetTypes()
        .Where(x => x.IsAssignableTo(typeof(Profile))).ToArray();

    foreach (var profile in profiles)
    {
        opt.AddProfile(profile);
    }
});

#endregion

#region Validators

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(ClientValidator).Assembly);

builder.Services.AddScoped<IPasswordValidator, PasswordValidator>();

#endregion

#region Database

builder.Services.AddDbContext<AppDbContext>(
    opt => opt.UseInMemoryDatabase("CleanServices.DB"));

#endregion

#region Services

builder.Services.AddScoped<IClientsService, ClientsService>();

builder.Services.AddScoped<IHasher, Sha256Hasher>();

#endregion

var app = builder.Build();

app.MapControllers();

app.Run();