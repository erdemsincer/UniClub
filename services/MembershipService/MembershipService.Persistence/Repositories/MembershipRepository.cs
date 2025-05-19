using MembershipService.Application.Interfaces;
using MembershipService.Domain.Entities;
using MembershipService.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace MembershipService.Persistence.Repositories;

public class MembershipRepository : IMembershipRepository
{
    private readonly ApplicationDbContext _context;

    public MembershipRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Membership>> GetByUserIdAsync(int userId)
    {
        return await _context.Memberships
            .Where(m => m.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<Membership>> GetByClubIdAsync(int clubId)
    {
        return await _context.Memberships
            .Where(m => m.ClubId == clubId)
            .ToListAsync();
    }

    public async Task<Membership?> GetMembershipAsync(int userId, int clubId)
    {
        return await _context.Memberships
            .FirstOrDefaultAsync(m => m.UserId == userId && m.ClubId == clubId);
    }

    public async Task AddAsync(Membership membership)
    {
        _context.Memberships.Add(membership);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Membership membership)
    {
        _context.Memberships.Remove(membership);
        await _context.SaveChangesAsync();
    }
}
