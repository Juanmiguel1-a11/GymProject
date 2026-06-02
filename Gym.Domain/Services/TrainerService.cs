using Gym.Domain.Entities;
using Gym.Domain.Enums;
using Gym.Domain.Interfaces.Repositories;
using Gym.Domain.Interfaces.Services;

namespace Gym.Domain.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepository _trainerRepository;

        public TrainerService(ITrainerRepository trainerRepository)
        {
            _trainerRepository = trainerRepository;
        }

        public async Task<IEnumerable<Trainer>> GetAllTrainersAsync()
            => await _trainerRepository.GetAllAsync();

        public async Task<Trainer?> GetTrainerByIdAsync(int id)
            => await _trainerRepository.GetByIdAsync(id);

        public async Task<Trainer?> GetTrainerByEmailAsync(string email)
            => await _trainerRepository.GetByEmailAsync(email);

        public async Task<IEnumerable<Trainer>> GetTrainersBySpecializationAsync(TrainerSpecialization specialization)
            => await _trainerRepository.GetBySpecializationAsync(specialization);

        public async Task<IEnumerable<Trainer>> GetActiveTrainersAsync()
            => await _trainerRepository.GetActiveTrainersAsync();

        public async Task<Trainer> CreateTrainerAsync(Trainer trainer)
        {
            // Validar campos requeridos
            if (string.IsNullOrWhiteSpace(trainer.FirstName))
                throw new ArgumentException("El nombre del entrenador es obligatorio.");

            if (string.IsNullOrWhiteSpace(trainer.LastName))
                throw new ArgumentException("El apellido del entrenador es obligatorio.");

            // Validar email único
            if (await _trainerRepository.EmailExistsAsync(trainer.Email))
                throw new InvalidOperationException($"Ya existe un entrenador registrado con el email '{trainer.Email}'.");

            // Validar tarifa positiva
            if (trainer.HourlyRate <= 0)
                throw new ArgumentException("La tarifa por hora debe ser mayor a 0.");

            trainer.IsActive = true;
            trainer.CreatedAt = DateTime.UtcNow;

            return await _trainerRepository.AddAsync(trainer);
        }

        public async Task<Trainer> UpdateTrainerAsync(Trainer trainer)
        {
            var existing = await _trainerRepository.GetByIdAsync(trainer.Id)
                ?? throw new KeyNotFoundException($"No se encontró el entrenador con Id {trainer.Id}.");

            // Validar email duplicado excluyendo al mismo entrenador
            if (await _trainerRepository.EmailExistsAsync(trainer.Email, trainer.Id))
                throw new InvalidOperationException($"Ya existe otro entrenador con el email '{trainer.Email}'.");

            if (string.IsNullOrWhiteSpace(trainer.FirstName))
                throw new ArgumentException("El nombre del entrenador es obligatorio.");

            if (string.IsNullOrWhiteSpace(trainer.LastName))
                throw new ArgumentException("El apellido del entrenador es obligatorio.");

            if (trainer.HourlyRate <= 0)
                throw new ArgumentException("La tarifa por hora debe ser mayor a 0.");

            // Actualizar propiedades del objeto trackeado por EF Core
            existing.FirstName = trainer.FirstName;
            existing.LastName = trainer.LastName;
            existing.Email = trainer.Email;
            existing.Phone = trainer.Phone;
            existing.Specialization = trainer.Specialization;
            existing.HourlyRate = trainer.HourlyRate;
            existing.UpdatedAt = DateTime.UtcNow;

            return await _trainerRepository.UpdateAsync(existing);
        }

        public async Task<bool> DeleteTrainerAsync(int id)
        {
            if (!await _trainerRepository.ExistsAsync(id))
                throw new KeyNotFoundException($"No se encontró el entrenador con Id {id}.");

            return await _trainerRepository.DeleteAsync(id);
        }

        public async Task<bool> ToggleTrainerStatusAsync(int id)
        {
            var trainer = await _trainerRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"No se encontró el entrenador con Id {id}.");

            trainer.IsActive = !trainer.IsActive;
            trainer.UpdatedAt = DateTime.UtcNow;

            await _trainerRepository.UpdateAsync(trainer);
            return trainer.IsActive;
        }
    }
}