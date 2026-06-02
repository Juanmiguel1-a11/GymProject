using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.Domain.Entities;
using Gym.Domain.Enums;
using DayOfWeek = Gym.Domain.Enums.DayOfWeek;

namespace Gym.Domain.Interfaces.Repositories
{
    public interface IGymClassRepository : IGenericRepository<GymClass>
    {
        Task<IEnumerable<GymClass>> GetByClassTypeAsync(ClassType classType);
        Task<IEnumerable<GymClass>> GetByDayOfWeekAsync(DayOfWeek dayOfWeek);
    }
}
