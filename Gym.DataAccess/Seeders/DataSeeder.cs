using System;
using Microsoft.EntityFrameworkCore;
using Gym.Domain.Entities;
using Gym.Domain.Enums;
using DayOfWeek = Gym.Domain.Enums.DayOfWeek;

namespace Gym.DataAccess.Seeders
{
    public static class DataSeeder
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            var now = new DateTime(2026, 6, 2, 12, 0, 0, DateTimeKind.Utc);

            // 1. Seed Trainers
            modelBuilder.Entity<Trainer>().HasData(
                new Trainer { Id = 1, FirstName = "Juan", LastName = "Perez", Email = "juan.perez@gym.com", Phone = "555-0001", Specialization = TrainerSpecialization.Cardio, HourlyRate = 35.00m, CreatedAt = now },
                new Trainer { Id = 2, FirstName = "Maria", LastName = "Lopez", Email = "maria.lopez@gym.com", Phone = "555-0002", Specialization = TrainerSpecialization.StrengthTraining, HourlyRate = 40.00m, CreatedAt = now },
                new Trainer { Id = 3, FirstName = "Carlos", LastName = "Garcia", Email = "carlos.garcia@gym.com", Phone = "555-0003", Specialization = TrainerSpecialization.Flexibility, HourlyRate = 30.00m, CreatedAt = now },
                new Trainer { Id = 4, FirstName = "Ana", LastName = "Martinez", Email = "ana.martinez@gym.com", Phone = "555-0004", Specialization = TrainerSpecialization.CrossTraining, HourlyRate = 45.00m, CreatedAt = now }
            );

            // 2. Seed Members
            modelBuilder.Entity<Member>().HasData(
                new Member { Id = 1, FirstName = "Pedro", LastName = "Gomez", Email = "pedro.gomez@mail.com", Phone = "555-1001", DateOfBirth = new DateTime(1990, 1, 15, 0, 0, 0, DateTimeKind.Utc), MembershipType = MembershipType.Basic, Status = MemberStatus.Active, CreatedAt = now },
                new Member { Id = 2, FirstName = "Laura", LastName = "Diaz", Email = "laura.diaz@mail.com", Phone = "555-1002", DateOfBirth = new DateTime(1995, 5, 20, 0, 0, 0, DateTimeKind.Utc), MembershipType = MembershipType.Silver, Status = MemberStatus.Active, CreatedAt = now },
                new Member { Id = 3, FirstName = "Jorge", LastName = "Ruiz", Email = "jorge.ruiz@mail.com", Phone = "555-1003", DateOfBirth = new DateTime(1985, 8, 10, 0, 0, 0, DateTimeKind.Utc), MembershipType = MembershipType.Gold, Status = MemberStatus.Active, CreatedAt = now },
                new Member { Id = 4, FirstName = "Sofia", LastName = "Hernandez", Email = "sofia.hernandez@mail.com", Phone = "555-1004", DateOfBirth = new DateTime(2000, 12, 5, 0, 0, 0, DateTimeKind.Utc), MembershipType = MembershipType.Basic, Status = MemberStatus.Inactive, CreatedAt = now }
            );

            // 3. Seed GymClasses
            modelBuilder.Entity<GymClass>().HasData(
                new GymClass { Id = 1, Name = "Cardio Extremo", ClassType = ClassType.Cardio, DayOfWeek = DayOfWeek.Monday, StartTime = new TimeSpan(8, 0, 0), DurationInMinutes = 60, MaxCapacity = 20, TrainerId = 1, CreatedAt = now },
                new GymClass { Id = 2, Name = "Pesas y Fuerza", ClassType = ClassType.StrengthTraining, DayOfWeek = DayOfWeek.Wednesday, StartTime = new TimeSpan(18, 0, 0), DurationInMinutes = 90, MaxCapacity = 15, TrainerId = 2, CreatedAt = now },
                new GymClass { Id = 3, Name = "Yoga Matinal", ClassType = ClassType.Yoga, DayOfWeek = DayOfWeek.Friday, StartTime = new TimeSpan(7, 0, 0), DurationInMinutes = 60, MaxCapacity = 25, TrainerId = 3, CreatedAt = now },
                new GymClass { Id = 4, Name = "Crossfit Challenge", ClassType = ClassType.Crossfit, DayOfWeek = DayOfWeek.Saturday, StartTime = new TimeSpan(10, 0, 0), DurationInMinutes = 60, MaxCapacity = 10, TrainerId = 4, CreatedAt = now }
            );

            // 4. Seed Memberships
            modelBuilder.Entity<Membership>().HasData(
                new Membership { Id = 1, MemberId = 1, Type = MembershipType.Basic, StartDate = now, EndDate = now.AddMonths(1), Price = 30.00m, CreatedAt = now },
                new Membership { Id = 2, MemberId = 2, Type = MembershipType.Silver, StartDate = now, EndDate = now.AddMonths(6), Price = 150.00m, CreatedAt = now },
                new Membership { Id = 3, MemberId = 3, Type = MembershipType.Gold, StartDate = now, EndDate = now.AddYears(1), Price = 300.00m, CreatedAt = now },
                new Membership { Id = 4, MemberId = 4, Type = MembershipType.Basic, StartDate = now.AddMonths(-2), EndDate = now.AddMonths(-1), Price = 30.00m, CreatedAt = now.AddMonths(-2) }
            );

            // 5. Seed Enrollments
            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { Id = 1, MemberId = 1, GymClassId = 1, EnrolledAt = now, Status = EnrollmentStatus.Active, CreatedAt = now },
                new Enrollment { Id = 2, MemberId = 2, GymClassId = 2, EnrolledAt = now, Status = EnrollmentStatus.Active, CreatedAt = now },
                new Enrollment { Id = 3, MemberId = 3, GymClassId = 3, EnrolledAt = now, Status = EnrollmentStatus.Active, CreatedAt = now },
                new Enrollment { Id = 4, MemberId = 4, GymClassId = 4, EnrolledAt = now.AddMonths(-1), Status = EnrollmentStatus.Cancelled, CreatedAt = now.AddMonths(-1) }
            );
        }
    }
}
