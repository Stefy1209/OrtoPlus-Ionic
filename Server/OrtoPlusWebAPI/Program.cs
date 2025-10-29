using System.Text;
using Business.Auth.Interfaces;
using Business.Auth.Models;
using Business.Auth.Services;
using Business.Hubs;
using Business.Profiles;
using Business.Service.Implementations;
using Business.Service.Interfaces;
using Data.Persistence;
using Data.Persistence.Seeders;
using Data.Repository.Implementations;
using Data.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrtoPlusWebAPI.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql("Host=localhost;Database=OrtoPlusDB;Username=stefy;Password=1234");
});

// Add Repositories
builder.Services.AddScoped<IClinicRepository, ClinicRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

// Add Services
builder.Services.AddScoped<IClinicService, ClinicService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Add AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AddressProfile>();
    cfg.AddProfile<ClinicProfile>();
    cfg.AddProfile<ReviewProfile>();
});

// Add Controllers
builder.Services.AddControllers();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowIonicApp", policy =>
    {
        policy.WithOrigins("http://localhost:8100")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()
              .SetPreflightMaxAge(TimeSpan.FromMinutes(10)); 
    });
});

// Add SignalR
builder.Services.AddSignalR();

// Register CustomUserIdProvider
builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

// Configure JWT Settings
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>()!;
builder.Services.AddSingleton(jwtSettings);

// Add JetGenerator
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/notificationHub"))
                {
                    context.Token = accessToken;
                }
                
                return Task.CompletedTask;
            }
        };
    });


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();

    // Seed the database
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await DatabaseSeeder.SeedAsync(dbContext);
}

app.UseCors("AllowIonicApp");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub");
app.Run();
