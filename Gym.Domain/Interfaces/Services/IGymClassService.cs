using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.Domain.Entities;
using Gym.Domain.Enums;
using DayOfWeek = Gym.Domain.Enums.DayOfWeek;

namespace Gym.Domain.Interfaces.Services
{
    public interface IGymClassService
    {
        Task<IEnumerable<GymClass>> GetAllAsync();
        Task<GymClass?> GetByIdAsync(int id);
        Task<IEnumerable<GymClass>> GetByClassTypeAsync(ClassType classType);
        Task<IEnumerable<GymClass>> GetByDayOfWeekAsync(DayOfWeek dayOfWeek);
        Task<GymClass> AddAsync(GymClass gymClass);
        Task<GymClass> UpdateAsync(GymClass gymClass);
        Task DeleteAsync(int id);
    }
}
