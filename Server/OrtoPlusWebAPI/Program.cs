using Business.Profiles;
using Business.Service.Implementations;
using Business.Service.Interfaces;
using Data.Persistence;
using Data.Persistence.Seeders;
using Data.Repository.Implementations;
using Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

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

// Add Services
builder.Services.AddScoped<IClinicService, ClinicService>();

// Add AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AddressProfile>();
    cfg.AddProfile<ClinicProfile>();
    cfg.AddProfile<ReviewProfile>();
});

// Add Controllers
builder.Services.AddControllers();

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

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
