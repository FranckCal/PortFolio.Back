using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Portfolio.Application.Interfaces;
using Portfolio.Application.Services;
using Portfolio.Infrastructure.Data;
using Portfolio.Infrastructure.Repositories;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ========================================
// 1. CONFIGURATION DES SERVICES (DI)
// ========================================

// Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Ignorer les références circulaires
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;

        // Optionnel : rendre le JSON plus lisible
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Swagger/Scalar pour la documentation API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// ========================================
// 2. DATABASE CONTEXT (Entity Framework)
// ========================================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Portfolio.Infrastructure")
    )
);

// ========================================
// 3. JWT AUTHENTICATION
// ========================================
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey not configured");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

// ========================================
// 4. CORS POLICY (pour Angular)
// ========================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// ========================================
// 5. DEPENDENCY INJECTION - APPLICATION SERVICES
// ========================================

// Services (Scoped = une instance par requête HTTP)
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IExperienceService, ExperienceService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBlogService, BlogService>();

// Repositories (Scoped)
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();

// Unit of Work Pattern
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// ========================================
// 6. AUTOMAPPER (Mapping DTOs) - DÉSACTIVÉ TEMPORAIREMENT
// ========================================
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// ========================================
// 7. CONFIGURATION PATTERN (Strongly Typed Settings)
// ========================================
builder.Services.Configure<JwtSettings>(jwtSettings);

// ========================================
// 8. LOGGING
// ========================================
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// ========================================
// 9. HEALTH CHECKS (optionnel mais pro) - DÉSACTIVÉ TEMPORAIREMENT
// ========================================
// Nécessite le package: dotnet add package AspNetCore.HealthChecks.SqlServer
// builder.Services.AddHealthChecks()
//     .AddDbContextCheck<ApplicationDbContext>();

var app = builder.Build();

// ========================================
// MIDDLEWARE PIPELINE
// ========================================

// Development
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); // Scalar UI (remplace Swagger)
}

app.UseHttpsRedirection();

// CORS avant Authentication/Authorization
app.UseCors("AllowAngular");

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Controllers
app.MapControllers();

// Health Checks endpoint (désactivé temporairement)
// app.MapHealthChecks("/health");

app.Run();

// ========================================
// CLASSES DE CONFIGURATION
// ========================================

public class JwtSettings
{
    public string SecretKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpirationMinutes { get; set; }
    public int RefreshTokenExpirationDays { get; set; }
}