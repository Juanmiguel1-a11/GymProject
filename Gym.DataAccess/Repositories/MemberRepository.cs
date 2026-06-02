using Gym.DataAccess.Context;
using Gym.Domain.Entities;
using Gym.Domain.Enums;
using Gym.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gym.DataAccess.Repositories
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        public MemberRepository(GymDbContext context) : base(context) { }

        public async Task<Member?> GetByEmailAsync(string email)
            => await _dbSet
                .FirstOrDefaultAsync(m => m.Email.ToLower() == email.ToLower());

        public async Task<IEnumerable<Member>> GetByStatusAsync(MemberStatus status)
            => await _dbSet
                .Where(m => m.Status == status)
                .ToListAsync();

        public async Task<IEnumerable<Member>> GetByMembershipTypeAsync(MembershipType membershipType)
            => await _dbSet
                .Where(m => m.MembershipType == membershipType)
                .ToListAsync();

        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
            => await _dbSet
                .AnyAsync(m => m.Email.ToLower() == email.ToLower()
                               && (excludeId == null || m.Id != excludeId));
    }
}
