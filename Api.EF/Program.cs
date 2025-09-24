using Api.EF.Api;
using Api.EF.Books.Data;
using Api.EF.Books.Services;
using Api.EF.Middleware;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Bookstore API",
        Version = "v1",
        Description = "An API for managing books and authors in a bookstore."
    });
});

// Database
builder.Services
    .AddDbContext<BookstoreDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("BookstoreDbConnection")));

// Register the BookService as a singleton as it keeps state in memory
builder.Services
    .AddTransient<BookService>()
    ;


var app = builder.Build();

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Bookstore API v1");
        options.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app
    .AddBookApi()
    .AddAuthorApi()
    .AddHealthApi()
    ;


app.Run();

// Make the Program class accessible for testing
public partial class Program { }
