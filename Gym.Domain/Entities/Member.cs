using Gym.Domain.Enums;

namespace Gym.Domain.Entities
{
    public class Member : AuditBase
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public MemberStatus Status { get; set; } = MemberStatus.Active;
        public MembershipType MembershipType { get; set; } = MembershipType.Basic;

        // 1:N con Enrollment
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        // 1:N con Membership
        public virtual ICollection<Membership> Memberships { get; set; } = new List<Membership>();
    }
}