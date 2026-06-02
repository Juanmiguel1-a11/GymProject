using Gym.Domain.Entities;
using Gym.Domain.Interfaces.Repositories;
using Gym.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Domain.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _repository;

        public EnrollmentService(IEnrollmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Enrollment>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Enrollment?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task<IEnumerable<Enrollment>> GetByMemberIdAsync(int memberId) => await _repository.GetByMemberIdAsync(memberId);

        public async Task<IEnumerable<Enrollment>> GetByGymClassIdAsync(int gymClassId) => await _repository.GetByGymClassIdAsync(gymClassId);

        public async Task<Enrollment> CreateAsync(Enrollment enrollment)
        {
            if (await _repository.ExistsAsync(enrollment.MemberId, enrollment.GymClassId))
                throw new InvalidOperationException("El miembro ya está inscrito en esta clase.");

            enrollment.EnrolledAt = DateTime.UtcNow;
            return await _repository.AddAsync(enrollment);
        }

        public async Task<Enrollment> UpdateAsync(Enrollment enrollment)
        {
            var existing = await _repository.GetByIdAsync(enrollment.Id)
                ?? throw new KeyNotFoundException($"Inscripción con Id {enrollment.Id} no encontrada.");

            existing.Status = enrollment.Status;
            existing.UpdatedAt = DateTime.UtcNow;
            
            await _repository.UpdateAsync(existing);
            return existing;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Inscripción con Id {id} no encontrada.");

            await _repository.DeleteAsync(id);
        }
    }
}
