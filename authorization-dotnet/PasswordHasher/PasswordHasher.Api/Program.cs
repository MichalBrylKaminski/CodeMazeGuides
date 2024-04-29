using Microsoft.AspNetCore.Identity;
using PasswordHasher.Api.Models;
using PasswordHasher.Api.Repositories;
using PasswordHasher.Api.Requests;
using PasswordHasher.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IUsersRepository, UsersRepository>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IPasswordHasher<RegisteredUser>, PasswordHasher<RegisteredUser>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/register", (RegisterRequest request, ICustomerService customerService) =>
    {
        customerService.RegisterUser(request);
        
        return Results.Created();
    })
    .WithName("RegisterUser")
    .WithOpenApi();

app.MapPost("/login", (LoginRequest request, ICustomerService customerService) =>
    {
        var result = customerService.Login(request);

        if (result == LoginResult.Success)
        {
            return Results.Ok();
        }

        return Results.Unauthorized();
    })
    .WithName("LoginUser")
    .WithOpenApi();

app.Run();