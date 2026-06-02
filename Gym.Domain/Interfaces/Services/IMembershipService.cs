using Gym.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Domain.Interfaces.Services
{
    public interface IMembershipService
    {
        Task<IEnumerable<Membership>> GetAllAsync();
        Task<Membership?> GetByIdAsync(int id);
        Task<IEnumerable<Membership>> GetByMemberIdAsync(int memberId);
        Task<Membership?> GetActiveMembershipByMemberIdAsync(int memberId);
        Task<Membership> CreateAsync(Membership membership);
        Task<Membership> UpdateAsync(Membership membership);
        Task DeleteAsync(int id);
    }
}
