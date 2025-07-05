using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ExtraHours.Core.Repositories;
using ExtraHours.Core.Services;
using ExtraHours.Infrastructure.Data;
using ExtraHours.Infrastructure.Repositories;
using ExtraHours.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// === DATABASE ===
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL")
                       ?? builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// === CORS ===
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// === AUTH JWT ===
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? "secret_key");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// === DEPENDENCY INJECTION ===
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IExtraHourService, ExtraHourService>();
builder.Services.AddScoped<IExtraHourRepository, ExtraHourRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IExtraHourTypeService, ExtraHourTypeService>();
builder.Services.AddScoped<IExtraHourTypeRepository, ExtraHourTypeRepository>();
builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddScoped<ISettingRepository, SettingRepository>();
builder.Services.AddScoped<ReportHoursService>();
builder.Services.AddScoped<EmailService>();

// === MVC, AUTH, SWAGGER ===
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ExtraHours API",
        Version = "v1"
    });
});

var app = builder.Build();

// === SWAGGER SIEMPRE HABILITADO ===
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExtraHours API V1");
    c.RoutePrefix = string.Empty; // Para que Swagger esté en la raíz "/"
});

// === MIDDLEWARES ===
app.UseCors("AllowAll");
// app.UseHttpsRedirection(); // Desactiva si no configuras HTTPS local o en Docker
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
