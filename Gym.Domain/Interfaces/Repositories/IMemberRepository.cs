using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.Domain.Entities;
using Gym.Domain.Enums;

namespace Gym.Domain.Interfaces.Repositories
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        Task<Member?> GetByEmailAsync(string email);
        Task<IEnumerable<Member>> GetByStatusAsync(MemberStatus status);
        Task<IEnumerable<Member>> GetByMembershipTypeAsync(MembershipType membershipType);
    }
}
