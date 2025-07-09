using System.Reflection;
using food_heaven_backend.Shared.Infraestructure.Persistence.Configuration;
using food_heaven_backend.Shared.Domain.Repositories;
using food_heaven_backend.Shared.Infraestructure.Persistence.Repositories;

using food_heaven_backend.PlanComidas.Domain.Services;

using food_heaven_backend.PlanComidas.Application.CommandServices;

using food_heaven_backend.PlanComidas.Application.QueryServices;

using food_heaven_backend.PlanComidas.Infrastructure;

using FluentValidation;

using food_heaven_backend.PlanComidas.Domain.Models.Commands;

using food_heaven_backend.PlanComidas.Domain.Models.Validators;
using food_heaven_backend.FoodCatalogContext.Application.CommandServices;
using food_heaven_backend.FoodCatalogContext.Application.QueryServices;
using food_heaven_backend.FoodCatalogContext.Domain;
using food_heaven_backend.FoodCatalogContext.Domain.Models.Commands;
using food_heaven_backend.FoodCatalogContext.Domain.Models.Validators;
using food_heaven_backend.FoodCatalogContext.Domain.Services;
using food_heaven_backend.FoodCatalogContext.Infraestructure;
using food_heaven_backend.Security.Application;
using food_heaven_backend.Security.Domain.Repositories;
using food_heaven_backend.Security.Domain.Service;
using food_heaven_backend.Security.Infraestrucutre;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Obtener cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configuración del contexto de base de datos con MySQL
builder.Services.AddDbContext<FoodHeavenContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Registro de controladores y Swagger
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Esto permite que el JSON en minúsculas funcione (como en tu mock)
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registro de servicios de dominio y aplicación
builder.Services.AddScoped<IProveedorCommandService, ProveedorCommandService>();
builder.Services.AddScoped<IProveedorQueryService, ProveedorQueryService>();
builder.Services.AddScoped<IComidaCommandService, ComidaCommandService>();
builder.Services.AddScoped<IComidaQueryService, ComidaQueryService>();

// Registro del repositorio de Usuario y UnitOfWork
builder.Services.AddScoped<IProveedorRepository, ProveedorRepository>();
builder.Services.AddScoped<IComidaRepository, ComidaRepository>();
builder.Services.AddScoped<IPlanComidaCommandService, PlanComidaCommandService>();
builder.Services.AddScoped<IPlanComidaQueryService, PlanComidaQueryService>();


// Registro del repositorio de Usuario y UnitOfWork
builder.Services.AddScoped<IPlanComidaRepository, PlanComidaRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Registro de validadores
builder.Services.AddScoped<IValidator<CreateProveedorCommand>, CreateProveedorCommandValidator>();
builder.Services.AddScoped<IValidator<CreateComidaCommand>, CreateComidaCommandValidator>();
builder.Services.AddScoped<IValidator<CreatePlanComidaCommand>, CreatePlanComidaCommandValidator>();

builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IHashService, HashService>();
builder.Services.AddScoped<IJwtEncryptService, JwtEncryptService>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "FoodHeaven App",
        Description = "APIs to handle data for FoodHeaven",
        TermsOfService = new Uri("https://foodheaven.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "FoodHeaven",
            Email = "foodheaven@example.com",
            Url = new Uri("https://foodheaven.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "FoodHeaven License",
            Url = new Uri("https://foodheaven.com/license")
        }
    });
    
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy", policy =>
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

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

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FoodHeavenContext>();
    context.Database.EnsureCreated();
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
app.UseCors("AllowAllPolicy");
app.MapControllers();
app.Run();
