using System;
using Gym.Domain.Enums;

namespace Gym.Domain.Entities
{
    public class Membership : AuditBase
    {
        public int MemberId { get; set; }
        public virtual Member? Member { get; set; }
        
        public MembershipType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public decimal Price { get; set; }
    }
}
