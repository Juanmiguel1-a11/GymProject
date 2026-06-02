using Gym.Domain.Enums;

namespace Gym.Api.DTOs.Response
{
    public class MemberResponseDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public MemberStatus Status { get; set; }
        public int? MembershipId { get; set; }
    }
}
