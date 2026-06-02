using Gym.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Domain.Interfaces.Repositories
{
    public interface IEnrollmentRepository : IGenericRepository<Enrollment>
    {
        Task<IEnumerable<Enrollment>> GetByMemberIdAsync(int memberId);
        Task<IEnumerable<Enrollment>> GetByGymClassIdAsync(int gymClassId);
        Task<bool> ExistsAsync(int memberId, int gymClassId);
    }
}
