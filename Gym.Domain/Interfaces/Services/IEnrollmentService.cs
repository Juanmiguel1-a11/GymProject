using Gym.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Domain.Interfaces.Services
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<Enrollment>> GetAllAsync();
        Task<Enrollment?> GetByIdAsync(int id);
        Task<IEnumerable<Enrollment>> GetByMemberIdAsync(int memberId);
        Task<IEnumerable<Enrollment>> GetByGymClassIdAsync(int gymClassId);
        Task<Enrollment> CreateAsync(Enrollment enrollment);
        Task<Enrollment> UpdateAsync(Enrollment enrollment);
        Task DeleteAsync(int id);
    }
}
