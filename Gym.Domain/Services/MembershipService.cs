using Gym.Domain.Entities;
using Gym.Domain.Interfaces.Repositories;
using Gym.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Domain.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepository _repository;

        public MembershipService(IMembershipRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Membership>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Membership?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task<IEnumerable<Membership>> GetByMemberIdAsync(int memberId) => await _repository.GetByMemberIdAsync(memberId);

        public async Task<Membership?> GetActiveMembershipByMemberIdAsync(int memberId) => await _repository.GetActiveMembershipByMemberIdAsync(memberId);

        public async Task<Membership> CreateAsync(Membership membership)
        {
            var active = await _repository.GetActiveMembershipByMemberIdAsync(membership.MemberId);
            if (active != null)
                throw new InvalidOperationException("El miembro ya tiene una membresía activa.");

            if (membership.EndDate <= membership.StartDate)
                throw new ArgumentException("La fecha de fin debe ser mayor a la fecha de inicio.");

            return await _repository.AddAsync(membership);
        }

        public async Task<Membership> UpdateAsync(Membership membership)
        {
            var existing = await _repository.GetByIdAsync(membership.Id)
                ?? throw new KeyNotFoundException($"Membresía con Id {membership.Id} no encontrada.");

            existing.Type = membership.Type;
            existing.StartDate = membership.StartDate;
            existing.EndDate = membership.EndDate;
            existing.IsActive = membership.IsActive;
            existing.Price = membership.Price;
            existing.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(existing);
            return existing;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Membresía con Id {id} no encontrada.");

            await _repository.DeleteAsync(id);
        }
    }
}
