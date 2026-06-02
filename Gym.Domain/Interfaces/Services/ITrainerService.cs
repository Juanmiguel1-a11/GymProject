using Gym.Domain.Entities;
using Gym.Domain.Enums;

namespace Gym.Domain.Interfaces.Services
{
    public interface ITrainerService
    {
        Task<IEnumerable<Trainer>> GetAllTrainersAsync();
        Task<Trainer?> GetTrainerByIdAsync(int id);
        Task<Trainer?> GetTrainerByEmailAsync(string email);
        Task<IEnumerable<Trainer>> GetTrainersBySpecializationAsync(TrainerSpecialization specialization);
        Task<IEnumerable<Trainer>> GetActiveTrainersAsync();
        Task<Trainer> CreateTrainerAsync(Trainer trainer);
        Task<Trainer> UpdateTrainerAsync(Trainer trainer);
        Task<bool> DeleteTrainerAsync(int id);
        Task<bool> ToggleTrainerStatusAsync(int id);
    }
}