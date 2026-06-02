using Gym.Domain.Enums;
using DayOfWeek = Gym.Domain.Enums.DayOfWeek;

namespace Gym.Api.DTOs.Request
{
    public class CreateGymClassRequest
    {
        public string Name { get; set; } = string.Empty;
        public ClassType ClassType { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public int DurationInMinutes { get; set; }
        public int MaxCapacity { get; set; }
    }
}
