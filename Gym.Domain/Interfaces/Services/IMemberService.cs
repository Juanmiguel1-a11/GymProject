using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.Domain.Entities;
using Gym.Domain.Enums;

namespace Gym.Domain.Interfaces.Services
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAllMembersAsync();
        Task<Member?> GetMemberByIdAsync(int id);
        Task<Member?> GetMemberByEmailAsync(string email);
        Task<IEnumerable<Member>> GetMembersByStatusAsync(MemberStatus status);
        Task<IEnumerable<Member>> GetMembersByMembershipTypeAsync(MembershipType membershipType);
        Task<Member> CreateMemberAsync(Member member);
        Task<Member> UpdateMemberAsync(Member member);
        Task DeleteMemberAsync(int id);
        Task ChangeMemberStatusAsync(int id, MemberStatus newStatus);
        Task UpgradeMembershipAsync(int id, MembershipType newType);
    }
}
