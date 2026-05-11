using LibraryAPI.WebAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString(nameof(LibraryDbContext))));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Library Management API",
        Version = "v1",
        Description = "REST API for Library Management System"
    });
});

// CORS — дозволяємо запити від статичного фронтенду
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API v1");
        c.RoutePrefix = "swagger";
    });
}

// Serve static files (wwwroot/index.html) — Етап 2.3
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors();

// app.UseHttpsRedirection(); // Вимкнено для Docker-розгортання без SSL
app.UseAuthorization();
app.MapControllers();

app.Run();

