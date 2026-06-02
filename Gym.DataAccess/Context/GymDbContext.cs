using Microsoft.EntityFrameworkCore;
using Gym.Domain.Entities;
using Gym.DataAccess.Seeders;

namespace Gym.DataAccess.Context
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {
        }

        public DbSet<GymClass> GymClasses { get; set; } = null!;
        public DbSet<Member> Members { get; set; } = null!;
        public DbSet<Trainer> Trainers { get; set; } = null!;
        public DbSet<Enrollment> Enrollments { get; set; } = null!;
        public DbSet<Membership> Memberships { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Member configuration
            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(m => m.LastName).IsRequired().HasMaxLength(100);
                entity.Property(m => m.Email).IsRequired().HasMaxLength(200);
                
                // 1:N Membership
                entity.HasMany(m => m.Memberships)
                      .WithOne(ms => ms.Member)
                      .HasForeignKey(ms => ms.MemberId)
                      .OnDelete(DeleteBehavior.Cascade);
                      
                // 1:N Enrollment
                entity.HasMany(m => m.Enrollments)
                      .WithOne(e => e.Member)
                      .HasForeignKey(e => e.MemberId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // GymClass configuration
            modelBuilder.Entity<GymClass>(entity =>
            {
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Name).IsRequired().HasMaxLength(150);
                entity.Property(g => g.DurationInMinutes).IsRequired();
                entity.Property(g => g.MaxCapacity).IsRequired();
                
                // 1:N Enrollment
                entity.HasMany(g => g.Enrollments)
                      .WithOne(e => e.GymClass)
                      .HasForeignKey(e => e.GymClassId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Trainer configuration
            modelBuilder.Entity<Trainer>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(t => t.LastName).IsRequired().HasMaxLength(100);
                entity.Property(t => t.Email).IsRequired().HasMaxLength(200);
                entity.Property(t => t.HourlyRate).HasColumnType("decimal(18,2)");
                
                // 1:N GymClasses
                entity.HasMany(t => t.GymClasses)
                      .WithOne(g => g.Trainer)
                      .HasForeignKey(g => g.TrainerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            
            // Enrollment configuration
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            
            // Membership configuration
            modelBuilder.Entity<Membership>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Price).HasColumnType("decimal(18,2)");
            });

            // Call Seeder
            modelBuilder.SeedData();
        }
    }
}
