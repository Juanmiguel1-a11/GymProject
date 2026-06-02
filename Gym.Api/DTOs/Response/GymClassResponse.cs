using Gym.Domain.Enums;
using DayOfWeek = Gym.Domain.Enums.DayOfWeek;

namespace Gym.Api.DTOs.Response
{
    public class GymClassResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ClassType { get; set; } = string.Empty;
        public string DayOfWeek { get; set; } = string.Empty;
        public TimeSpan StartTime { get; set; }
        public int DurationInMinutes { get; set; }
        public int MaxCapacity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
