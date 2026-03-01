using FluentValidation;
using Microsoft.AspNetCore.Identity;
using static ResultDotNet.Examples.AspNetCoreApi.Controllers.ValidateController;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IValidator<ValidateInputDto>, ValidateInputDtoValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
