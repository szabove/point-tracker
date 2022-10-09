using API.Extension;
using Application;
using Application.User;
using FluentValidation;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<GetAllUsersCommand.Handler>();

builder.Services.AddApplicationLayerDependencies();
builder.Services.AddInfrastructureLayerDependencies();

var app = builder.Build();

app.UseErrorHandlingMiddleware();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
