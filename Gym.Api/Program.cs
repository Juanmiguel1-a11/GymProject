using Microsoft.EntityFrameworkCore;
using Gym.DataAccess.Context;
using Gym.DataAccess.Repositories;
using Gym.Domain.Interfaces.Repositories;
using Gym.Domain.Interfaces.Services;
using Gym.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<GymDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCors();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Repositories
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IGymClassRepository, GymClassRepository>();
builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IMembershipRepository, MembershipRepository>();

// Services
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IGymClassService, GymClassService>();
builder.Services.AddScoped<ITrainerService, TrainerService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IMembershipService, MembershipService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});
var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

// Seed Data on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GymDbContext>();
    context.Database.EnsureCreated();
}

app.Run();
