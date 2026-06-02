using System;
using Gym.Domain.Enums;

namespace Gym.Domain.Entities
{
    public class Enrollment : AuditBase
    {
        public int MemberId { get; set; }
        public virtual Member? Member { get; set; }
        
        public int GymClassId { get; set; }
        public virtual GymClass? GymClass { get; set; }
        
        public DateTime EnrolledAt { get; set; }
        public EnrollmentStatus Status { get; set; }
    }
}
