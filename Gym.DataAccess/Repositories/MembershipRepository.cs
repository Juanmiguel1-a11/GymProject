using Gym.DataAccess.Context;
using Gym.Domain.Entities;
using Gym.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.DataAccess.Repositories
{
    public class MembershipRepository : GenericRepository<Membership>, IMembershipRepository
    {
        public MembershipRepository(GymDbContext context) : base(context) { }

        public async Task<IEnumerable<Membership>> GetByMemberIdAsync(int memberId)
        {
            return await _dbSet
                .Where(m => m.MemberId == memberId)
                .ToListAsync();
        }

        public async Task<Membership?> GetActiveMembershipByMemberIdAsync(int memberId)
        {
            return await _dbSet
                .Where(m => m.MemberId == memberId && m.IsActive)
                .FirstOrDefaultAsync();
        }
    }
}
