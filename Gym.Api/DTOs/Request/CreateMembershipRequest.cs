using Gym.Domain.Enums;
using System;

namespace Gym.Api.DTOs.Request
{
    public class CreateMembershipRequest
    {
        public int MemberId { get; set; }
        public MembershipType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
    }
}
