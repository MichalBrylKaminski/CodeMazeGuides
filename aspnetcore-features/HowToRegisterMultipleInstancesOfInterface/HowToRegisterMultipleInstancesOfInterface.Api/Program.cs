using HowToRegisterMultipleInstancesOfInterface.Interfaces;
using HowToRegisterMultipleInstancesOfInterface.Processors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IPostalFulfillmentProcessor, PostalFulfillmentProcessor>();
builder.Services.AddTransient<IBarcodeFulfillmentProcessor, BarcodeFulfillmentProcessor>();
builder.Services.AddTransient<ISmartcardFulfillmentProcessor, SmartcardFulfillmentProcessor>();

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