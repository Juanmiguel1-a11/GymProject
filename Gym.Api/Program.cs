using Microsoft.EntityFrameworkCore;
using Gym.DataAccess.Context;
using Gym.DataAccess.Repositories;
using Gym.Domain.Interfaces.Repositories;
using Gym.Domain.Interfaces.Services;
using Gym.Domain.Services;
using Gym.DataAccess.Seeders;

var builder = WebApplication.CreateBuilder(args);


// ── Entity Framework Core ──

builder.Services.AddDbContext<GymDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


// ── Repositories ──

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IGymClassRepository, GymClassRepository>();
builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IMembershipRepository, MembershipRepository>();


// ── Services ──

builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IGymClassService, GymClassService>();
builder.Services.AddScoped<ITrainerService, TrainerService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IMembershipService, MembershipService>();


// ── AutoMapper ──

builder.Services.AddAutoMapper(typeof(Program).Assembly);


// ── Controllers ──

builder.Services.AddControllers();

// ── Swagger ──
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();

// ── Data Seeder ── 
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<GymDbContext>();

    await context.Database.MigrateAsync(); // Crea la BD + aplica migraciones 
    await DataSeeder.SeedAsync(context);
}

// ── Middleware Pipeline ──

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.MapGet("/", () => Results.Redirect("/swagger"));
app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.UseAuthorization();
app.MapControllers();
app.Run();
