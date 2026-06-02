using Gym.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Domain.Interfaces.Repositories
{
    public interface IMembershipRepository : IGenericRepository<Membership>
    {
        Task<IEnumerable<Membership>> GetByMemberIdAsync(int memberId);
        Task<Membership?> GetActiveMembershipByMemberIdAsync(int memberId);
    }
}
