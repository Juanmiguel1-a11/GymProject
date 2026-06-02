using Gym.DataAccess.Context;
using Gym.Domain.Entities;
using Gym.Domain.Enums;
using Gym.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gym.DataAccess.Repositories
{
    public class TrainerRepository : GenericRepository<Trainer>, ITrainerRepository
    {
        public TrainerRepository(GymDbContext context) : base(context) { }

        public async Task<Trainer?> GetByEmailAsync(string email)
            => await _dbSet
                .FirstOrDefaultAsync(t => t.Email.ToLower() == email.ToLower());

        public async Task<IEnumerable<Trainer>> GetBySpecializationAsync(TrainerSpecialization specialization)
            => await _dbSet
                .Where(t => t.Specialization == specialization)
                .ToListAsync();

        public async Task<IEnumerable<Trainer>> GetActiveTrainersAsync()
            => await _dbSet
                .Where(t => t.IsActive)
                .ToListAsync();

        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
            => await _dbSet
                .AnyAsync(t => t.Email.ToLower() == email.ToLower()
                               && (excludeId == null || t.Id != excludeId));
    }
}
