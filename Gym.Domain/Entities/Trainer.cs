using Gym.Domain.Enums;
namespace Gym.Domain.Entities
{
    public class Trainer : AuditBase
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public TrainerSpecialization Specialization { get; set; }
        public decimal HourlyRate { get; set; }
        public bool IsActive { get; set; } = true;

        // Relaciones
        public ICollection<GymClass> GymClasses { get; set; } = new List<GymClass>();
    }
}