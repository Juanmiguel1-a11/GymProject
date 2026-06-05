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


builder.Services.AddCors();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.MapGet("/", () => Results.Redirect("/swagger"));
app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();
app.MapControllers();
app.Run();
