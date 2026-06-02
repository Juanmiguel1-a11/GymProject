using Gym.Domain.Enums;
using DayOfWeek = Gym.Domain.Enums.DayOfWeek;

namespace Gym.Domain.Entities
{
    public class GymClass : AuditBase
    {
        public string Name { get; set; } = string.Empty;
        public ClassType ClassType { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public int DurationInMinutes { get; set; }
        public int MaxCapacity { get; set; }

        // FK a Trainer
        public int TrainerId { get; set; }
        public virtual Trainer? Trainer { get; set; }

        // 1:N con Enrollment
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}

