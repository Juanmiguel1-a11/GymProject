using Gym.Domain.Enums;

namespace Gym.Api.DTOs.Request
{
    public class ChangeMemberStatusRequest
    {
        public MemberStatus NewStatus { get; set; }
    }
}
