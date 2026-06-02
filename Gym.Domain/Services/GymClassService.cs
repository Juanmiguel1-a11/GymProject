using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.Domain.Entities;
using Gym.Domain.Enums;
using Gym.Domain.Interfaces.Repositories;
using Gym.Domain.Interfaces.Services;
using DayOfWeek = Gym.Domain.Enums.DayOfWeek;

namespace Gym.Domain.Services
{
    public class GymClassService : IGymClassService
    {
        private readonly IGymClassRepository _repository;

        public GymClassService(IGymClassRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GymClass>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<GymClass?> GetByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task<IEnumerable<GymClass>> GetByClassTypeAsync(ClassType classType) =>
            await _repository.GetByClassTypeAsync(classType);

        public async Task<IEnumerable<GymClass>> GetByDayOfWeekAsync(DayOfWeek dayOfWeek) =>
            await _repository.GetByDayOfWeekAsync(dayOfWeek);

        public async Task<GymClass> AddAsync(GymClass gymClass)
        {
            if (gymClass.DurationInMinutes <= 0)
                throw new ArgumentException("La duración debe ser mayor a 0 minutos.");
            if (gymClass.MaxCapacity <= 0)
                throw new ArgumentException("La capacidad máxima debe ser mayor a 0.");
            return await _repository.AddAsync(gymClass);
        }

        public async Task<GymClass> UpdateAsync(GymClass gymClass)
        {
            var existing = await _repository.GetByIdAsync(gymClass.Id)
                ?? throw new KeyNotFoundException($"Clase con Id {gymClass.Id} no encontrada.");
            existing.Name = gymClass.Name;
            existing.ClassType = gymClass.ClassType;
            existing.TrainerId = gymClass.TrainerId;
            existing.DayOfWeek = gymClass.DayOfWeek;
            existing.StartTime = gymClass.StartTime;
            existing.DurationInMinutes = gymClass.DurationInMinutes;
            existing.MaxCapacity = gymClass.MaxCapacity;
            existing.UpdatedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(existing);
            return existing;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Clase con Id {id} no encontrada.");
            await _repository.DeleteAsync(id);
        }
    }
}
