using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gym.DataAccess.Context;
using Gym.Domain.Entities;
using Gym.Domain.Enums;
using Gym.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using DayOfWeek = Gym.Domain.Enums.DayOfWeek;

namespace Gym.DataAccess.Repositories
{
    public class GymClassRepository : GenericRepository<GymClass>, IGymClassRepository
    {
        public GymClassRepository(GymDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<GymClass>> GetByClassTypeAsync(ClassType classType)
        {
            return await _dbSet.Where(c => c.ClassType == classType).ToListAsync();
        }

        public async Task<IEnumerable<GymClass>> GetByDayOfWeekAsync(DayOfWeek dayOfWeek)
        {
            return await _dbSet.Where(c => c.DayOfWeek == dayOfWeek).ToListAsync();
        }
    }
}
