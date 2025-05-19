using MembershipService.Domain.Entities;

namespace MembershipService.Application.Interfaces;

public interface IMembershipRepository
{
    Task<List<Membership>> GetByUserIdAsync(int userId);
    Task<List<Membership>> GetByClubIdAsync(int clubId);
    Task<Membership?> GetMembershipAsync(int userId, int clubId);
    Task AddAsync(Membership membership);
    Task DeleteAsync(Membership membership);
}
