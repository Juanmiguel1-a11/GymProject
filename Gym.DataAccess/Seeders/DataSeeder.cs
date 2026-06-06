using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gym.Domain.Entities;
using Gym.Domain.Enums;
using Gym.DataAccess.Context;
using DayOfWeek = Gym.Domain.Enums.DayOfWeek;

namespace Gym.DataAccess.Seeders;

public static class DataSeeder
{
    public static async Task SeedAsync(GymDbContext context)
    {
        // Solo ejecutar si no hay inscripciones (Enrollments)
        if (await context.Enrollments.AnyAsync()) return;

        // Si ya hay entrenadores (por ejemplo de una corrida anterior que falló a medias), limpiamos la BD
        if (await context.Trainers.AnyAsync())
        {
            context.Enrollments.RemoveRange(context.Enrollments);
            context.Memberships.RemoveRange(context.Memberships);
            context.GymClasses.RemoveRange(context.GymClasses);
            context.Members.RemoveRange(context.Members);
            context.Trainers.RemoveRange(context.Trainers);
            await context.SaveChangesAsync();
        }

        var now = DateTime.UtcNow;

        // ═══ 1. ENTRENADORES ═══
        var trainers = new List<Trainer>
        {
            new() { FirstName="Juan", LastName="Perez", Email="juan.perez@gym.com", Phone="555-0001", Specialization=TrainerSpecialization.Cardio, HourlyRate=35.00m, IsActive=true, CreatedAt=now },
            new() { FirstName="Maria", LastName="Lopez", Email="maria.lopez@gym.com", Phone="555-0002", Specialization=TrainerSpecialization.StrengthTraining, HourlyRate=40.00m, IsActive=true, CreatedAt=now },
            new() { FirstName="Carlos", LastName="Garcia", Email="carlos.garcia@gym.com", Phone="555-0003", Specialization=TrainerSpecialization.Flexibility, HourlyRate=30.00m, IsActive=true, CreatedAt=now },
            new() { FirstName="Ana", LastName="Martinez", Email="ana.martinez@gym.com", Phone="555-0004", Specialization=TrainerSpecialization.CrossTraining, HourlyRate=45.00m, IsActive=true, CreatedAt=now },
            new() { FirstName="David", LastName="Gomez", Email="david.gomez@gym.com", Phone="555-0005", Specialization=TrainerSpecialization.PersonalTraining, HourlyRate=50.00m, IsActive=true, CreatedAt=now }
        };

        context.Trainers.AddRange(trainers);
        await context.SaveChangesAsync();

        // ═══ 2. MIEMBROS ═══
        var members = new List<Member>
        {
            new() { FirstName="Pedro", LastName="Gomez", Email="pedro.gomez@mail.com", Phone="555-1001", DateOfBirth=new DateTime(1990, 1, 15, 0, 0, 0, DateTimeKind.Utc), MembershipType=MembershipType.Basic, Status=MemberStatus.Active, CreatedAt=now },
            new() { FirstName="Laura", LastName="Diaz", Email="laura.diaz@mail.com", Phone="555-1002", DateOfBirth=new DateTime(1995, 5, 20, 0, 0, 0, DateTimeKind.Utc), MembershipType=MembershipType.Silver, Status=MemberStatus.Active, CreatedAt=now },
            new() { FirstName="Jorge", LastName="Ruiz", Email="jorge.ruiz@mail.com", Phone="555-1003", DateOfBirth=new DateTime(1985, 8, 10, 0, 0, 0, DateTimeKind.Utc), MembershipType=MembershipType.Gold, Status=MemberStatus.Active, CreatedAt=now },
            new() { FirstName="Sofia", LastName="Hernandez", Email="sofia.hernandez@mail.com", Phone="555-1004", DateOfBirth=new DateTime(2000, 12, 5, 0, 0, 0, DateTimeKind.Utc), MembershipType=MembershipType.Basic, Status=MemberStatus.Inactive, CreatedAt=now },
            new() { FirstName="Diego", LastName="Torres", Email="diego.torres@mail.com", Phone="555-1005", DateOfBirth=new DateTime(1992, 3, 25, 0, 0, 0, DateTimeKind.Utc), MembershipType=MembershipType.Gold, Status=MemberStatus.Active, CreatedAt=now },
            new() { FirstName="Valentina", LastName="Rojas", Email="valentina.rojas@mail.com", Phone="555-1006", DateOfBirth=new DateTime(1998, 7, 30, 0, 0, 0, DateTimeKind.Utc), MembershipType=MembershipType.Silver, Status=MemberStatus.Active, CreatedAt=now }
        };

        context.Members.AddRange(members);
        await context.SaveChangesAsync();

        // ═══ 3. CLASES DEL GIMNASIO ═══
        var gymClasses = new List<GymClass>
        {
            new() { Name="Cardio Extremo", ClassType=ClassType.Cardio, DayOfWeek=DayOfWeek.Monday, StartTime=new TimeSpan(8, 0, 0), DurationInMinutes=60, MaxCapacity=20, TrainerId=trainers[0].Id, CreatedAt=now },
            new() { Name="Pesas y Fuerza", ClassType=ClassType.StrengthTraining, DayOfWeek=DayOfWeek.Wednesday, StartTime=new TimeSpan(18, 0, 0), DurationInMinutes=90, MaxCapacity=15, TrainerId=trainers[1].Id, CreatedAt=now },
            new() { Name="Yoga Matinal", ClassType=ClassType.Yoga, DayOfWeek=DayOfWeek.Friday, StartTime=new TimeSpan(7, 0, 0), DurationInMinutes=60, MaxCapacity=25, TrainerId=trainers[2].Id, CreatedAt=now },
            new() { Name="Crossfit Challenge", ClassType=ClassType.Crossfit, DayOfWeek=DayOfWeek.Saturday, StartTime=new TimeSpan(10, 0, 0), DurationInMinutes=60, MaxCapacity=10, TrainerId=trainers[3].Id, CreatedAt=now },
            new() { Name="Pilates para Todos", ClassType=ClassType.Pilates, DayOfWeek=DayOfWeek.Tuesday, StartTime=new TimeSpan(9, 0, 0), DurationInMinutes=45, MaxCapacity=15, TrainerId=trainers[2].Id, CreatedAt=now },
            new() { Name="Zumba Party", ClassType=ClassType.Zumba, DayOfWeek=DayOfWeek.Thursday, StartTime=new TimeSpan(19, 0, 0), DurationInMinutes=60, MaxCapacity=30, TrainerId=trainers[0].Id, CreatedAt=now }
        };

        context.GymClasses.AddRange(gymClasses);
        await context.SaveChangesAsync();

        // ═══ 4. MEMBRESÍAS ═══
        var memberships = new List<Membership>
        {
            new() { MemberId=members[0].Id, Type=MembershipType.Basic, StartDate=now, EndDate=now.AddMonths(1), Price=30.00m, IsActive=true, CreatedAt=now },
            new() { MemberId=members[1].Id, Type=MembershipType.Silver, StartDate=now, EndDate=now.AddMonths(6), Price=150.00m, IsActive=true, CreatedAt=now },
            new() { MemberId=members[2].Id, Type=MembershipType.Gold, StartDate=now, EndDate=now.AddYears(1), Price=300.00m, IsActive=true, CreatedAt=now },
            new() { MemberId=members[3].Id, Type=MembershipType.Basic, StartDate=now.AddMonths(-2), EndDate=now.AddMonths(-1), Price=30.00m, IsActive=false, CreatedAt=now.AddMonths(-2) },
            new() { MemberId=members[4].Id, Type=MembershipType.Gold, StartDate=now, EndDate=now.AddYears(1), Price=300.00m, IsActive=true, CreatedAt=now },
            new() { MemberId=members[5].Id, Type=MembershipType.Silver, StartDate=now, EndDate=now.AddMonths(6), Price=150.00m, IsActive=true, CreatedAt=now }
        };

        context.Memberships.AddRange(memberships);
        await context.SaveChangesAsync();

        // ═══ 5. INSCRIPCIONES A CLASES ═══
        var enrollments = new List<Enrollment>
        {
            new() { MemberId=members[0].Id, GymClassId=gymClasses[0].Id, EnrolledAt=now, Status=EnrollmentStatus.Active, CreatedAt=now },
            new() { MemberId=members[1].Id, GymClassId=gymClasses[1].Id, EnrolledAt=now, Status=EnrollmentStatus.Active, CreatedAt=now },
            new() { MemberId=members[2].Id, GymClassId=gymClasses[2].Id, EnrolledAt=now, Status=EnrollmentStatus.Active, CreatedAt=now },
            new() { MemberId=members[3].Id, GymClassId=gymClasses[3].Id, EnrolledAt=now.AddMonths(-1), Status=EnrollmentStatus.Cancelled, CreatedAt=now.AddMonths(-1) },
            new() { MemberId=members[4].Id, GymClassId=gymClasses[4].Id, EnrolledAt=now, Status=EnrollmentStatus.Active, CreatedAt=now },
            new() { MemberId=members[5].Id, GymClassId=gymClasses[5].Id, EnrolledAt=now, Status=EnrollmentStatus.Active, CreatedAt=now },
            new() { MemberId=members[0].Id, GymClassId=gymClasses[5].Id, EnrolledAt=now, Status=EnrollmentStatus.Active, CreatedAt=now }, // Pedro en Zumba también
            new() { MemberId=members[2].Id, GymClassId=gymClasses[0].Id, EnrolledAt=now, Status=EnrollmentStatus.Active, CreatedAt=now }  // Jorge en Cardio también
        };

        context.Enrollments.AddRange(enrollments);
        await context.SaveChangesAsync();
    }
}
