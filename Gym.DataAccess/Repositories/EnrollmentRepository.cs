using Gym.DataAccess.Context;
using Gym.Domain.Entities;
using Gym.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.DataAccess.Repositories
{
    public class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(GymDbContext context) : base(context) { }

        public async Task<IEnumerable<Enrollment>> GetByMemberIdAsync(int memberId)
        {
            return await _dbSet
                .Include(e => e.GymClass)
                .Where(e => e.MemberId == memberId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetByGymClassIdAsync(int gymClassId)
        {
            return await _dbSet
                .Include(e => e.Member)
                .Where(e => e.GymClassId == gymClassId)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int memberId, int gymClassId)
        {
            return await _dbSet.AnyAsync(e => e.MemberId == memberId && e.GymClassId == gymClassId);
        }
    }
}
