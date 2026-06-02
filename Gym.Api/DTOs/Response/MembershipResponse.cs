using System;

namespace Gym.Api.DTOs.Response
{
    public class MembershipResponse
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string Type { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public decimal Price { get; set; }
    }
}
