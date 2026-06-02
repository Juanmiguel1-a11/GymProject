using Gym.Domain.Entities;
using Gym.Domain.Enums;

namespace Gym.Domain.Interfaces.Repositories
{
    public interface ITrainerRepository : IGenericRepository<Trainer>
    {
        Task<Trainer?> GetByEmailAsync(string email);
        Task<IEnumerable<Trainer>> GetBySpecializationAsync(TrainerSpecialization specialization);
        Task<IEnumerable<Trainer>> GetActiveTrainersAsync();
        Task<bool> EmailExistsAsync(string email, int? excludeId = null);
    }
}
