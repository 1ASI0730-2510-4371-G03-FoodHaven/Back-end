using food_heaven_backend.Shared.Infraestructure.Persistence.Configuration;
using food_heaven_backend.Shared.Domain.Repositories;
using food_heaven_backend.Shared.Infraestructure.Persistence.Repositories;
using food_heaven_backend.Usuarios.Domain.Services;
using food_heaven_backend.Usuarios.Application.CommandServices;
using food_heaven_backend.Usuarios.Application.QueryServices;
using food_heaven_backend.Usuarios.Domain;
using food_heaven_backend.Usuarios.Infraestructure;
using FluentValidation;
using food_heaven_backend.Usuarios.Domain.Models.Commands;
using food_heaven_backend.Usuarios.Domain.Models.Validators;
using food_heaven_backend.Usuarios.Infraestructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Obtener cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configuración del contexto de base de datos con MySQL
builder.Services.AddDbContext<FoodHeavenContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Registro de controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registro de servicios de dominio y aplicación
builder.Services.AddScoped<IUsuarioQueryService, UsuarioQueryService>();
builder.Services.AddScoped<IUsuarioCommandService, UsuarioCommandService>();

// Registro del repositorio de Usuario y UnitOfWork
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Registro de validadores
builder.Services.AddScoped<IValidator<CreateUsuarioCommand>, CreateUsuarioCommandValidator>();

// Build de la aplicación
var app = builder.Build();

// Middleware pipeline
// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirigir la raíz '/' a Swagger
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger/index.html");
        return;
    }
    await next();
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
