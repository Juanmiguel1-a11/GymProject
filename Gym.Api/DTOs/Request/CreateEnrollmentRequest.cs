using Gym.Domain.Enums;

namespace Gym.Api.DTOs.Request
{
    public class CreateEnrollmentRequest
    {
        public int MemberId { get; set; }
        public int GymClassId { get; set; }
        public EnrollmentStatus Status { get; set; }
    }
}
