using Gym.Domain.Enums;
using System;

namespace Gym.Api.DTOs.Response
{
    public class EnrollmentResponse
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int GymClassId { get; set; }
        public DateTime EnrolledAt { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
