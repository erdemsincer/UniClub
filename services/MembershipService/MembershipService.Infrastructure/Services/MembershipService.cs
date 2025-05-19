using MembershipService.Application.Dtos;
using MembershipService.Application.Interfaces;
using MembershipService.Domain.Entities;

namespace MembershipService.Infrastructure.Services;

public class MembershipService : IMembershipService
{
    private readonly IMembershipRepository _repository;

    public MembershipService(IMembershipRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> JoinClubAsync(int userId, int clubId)
    {
        var exists = await _repository.GetMembershipAsync(userId, clubId);
        if (exists != null) return false;

        var membership = new Membership
        {
            UserId = userId,
            ClubId = clubId
        };

        await _repository.AddAsync(membership);
        return true;
    }

    public async Task<bool> LeaveClubAsync(int userId, int clubId)
    {
        var membership = await _repository.GetMembershipAsync(userId, clubId);
        if (membership == null) return false;

        await _repository.DeleteAsync(membership);
        return true;
    }

    public async Task<List<MembershipDto>> GetMyMembershipsAsync(int userId)
    {
        var list = await _repository.GetByUserIdAsync(userId);
        return list.Select(m => new MembershipDto
        {
            Id = m.Id,
            UserId = m.UserId,
            ClubId = m.ClubId,
            IsAdmin = m.IsAdmin,
            JoinedAt = m.JoinedAt
        }).ToList();
    }

    public async Task<List<MembershipDto>> GetMembersByClubIdAsync(int clubId)
    {
        var list = await _repository.GetByClubIdAsync(clubId);
        return list.Select(m => new MembershipDto
        {
            Id = m.Id,
            UserId = m.UserId,
            ClubId = m.ClubId,
            IsAdmin = m.IsAdmin,
            JoinedAt = m.JoinedAt
        }).ToList();
    }
}
