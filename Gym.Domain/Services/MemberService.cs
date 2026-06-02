using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.Domain.Entities;
using Gym.Domain.Enums;
using Gym.Domain.Interfaces.Repositories;
using Gym.Domain.Interfaces.Services;

namespace Gym.Domain.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _repository;

        public MemberService(IMemberRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Member>> GetAllMembersAsync() =>
            await _repository.GetAllAsync();

        public async Task<Member?> GetMemberByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task<Member?> GetMemberByEmailAsync(string email) =>
            await _repository.GetByEmailAsync(email);

        public async Task<IEnumerable<Member>> GetMembersByStatusAsync(MemberStatus status) =>
            await _repository.GetByStatusAsync(status);

        public async Task<IEnumerable<Member>> GetMembersByMembershipTypeAsync(MembershipType membershipType) =>
            await _repository.GetByMembershipTypeAsync(membershipType);

        public async Task<Member> CreateMemberAsync(Member member)
        {
            var existing = await _repository.GetByEmailAsync(member.Email);
            if (existing != null)
                throw new InvalidOperationException($"Ya existe un miembro con el email '{member.Email}'.");
            return await _repository.AddAsync(member);
        }

        public async Task<Member> UpdateMemberAsync(Member member)
        {
            var existing = await _repository.GetByIdAsync(member.Id)
                ?? throw new KeyNotFoundException($"Miembro con Id {member.Id} no encontrado.");
            existing.FirstName = member.FirstName;
            existing.LastName = member.LastName;
            existing.Email = member.Email;
            existing.Phone = member.Phone;
            existing.DateOfBirth = member.DateOfBirth;
            existing.MembershipType = member.MembershipType;
            existing.UpdatedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(existing);
            return existing;
        }

        public async Task DeleteMemberAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Miembro con Id {id} no encontrado.");
            await _repository.DeleteAsync(id);
        }

        public async Task ChangeMemberStatusAsync(int id, MemberStatus newStatus)
        {
            var member = await _repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Miembro con Id {id} no encontrado.");
            member.Status = newStatus;
            member.UpdatedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(member);
        }

        public async Task UpgradeMembershipAsync(int id, MembershipType newType)
        {
            var member = await _repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Miembro con Id {id} no encontrado.");
            member.MembershipType = newType;
            member.UpdatedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(member);
        }
    }
}