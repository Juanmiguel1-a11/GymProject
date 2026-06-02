using Gym.Domain.Enums;

namespace Gym.Api.DTOs.Request
{
    public class UpdateTrainerRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public TrainerSpecialization Specialization { get; set; }
        public decimal HourlyRate { get; set; }
    }
}
