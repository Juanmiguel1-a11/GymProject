using Gym.Domain.Enums;

namespace Gym.Api.DTOs.Request
{
    public class CreateMemberRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public MembershipType MembershipType { get; set; } = MembershipType.Basic;
    }
}
