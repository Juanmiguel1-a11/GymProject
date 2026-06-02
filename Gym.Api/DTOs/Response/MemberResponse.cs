using Gym.Domain.Enums;

namespace Gym.Api.DTOs.Response
{
    public class MemberResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Status { get; set; } = string.Empty;
        public string MembershipType { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
