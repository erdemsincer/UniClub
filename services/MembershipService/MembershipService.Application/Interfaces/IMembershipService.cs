using MembershipService.Application.Dtos;

namespace MembershipService.Application.Interfaces;

public interface IMembershipService
{
    Task<bool> JoinClubAsync(int userId, int clubId);
    Task<bool> LeaveClubAsync(int userId, int clubId);
    Task<List<MembershipDto>> GetMyMembershipsAsync(int userId);
    Task<List<MembershipDto>> GetMembersByClubIdAsync(int clubId);
}
