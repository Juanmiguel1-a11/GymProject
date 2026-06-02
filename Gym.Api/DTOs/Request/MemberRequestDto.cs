using Gym.Domain.Enums;

namespace Gym.Api.DTOs.Request
{
    public class MemberRequestDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public MemberStatus Status { get; set; }
        public int? MembershipId { get; set; }
    }
}
