using Gym.Domain.Enums;

namespace Gym.Api.DTOs.Request
{
    public class UpgradeMembershipRequest
    {
        public MembershipType NewMembershipType { get; set; }
    }
}
