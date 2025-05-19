using ClubService.Application.Interfaces;
using ClubService.Domain.Entities;
using ClubService.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace ClubService.Persistence.Repositories;

public class ClubRepository : IClubRepository
{
    private readonly ApplicationDbContext _context;

    public ClubRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Club>> GetAllAsync()
        => await _context.Clubs.ToListAsync();

    public async Task<Club?> GetByIdAsync(int id)
        => await _context.Clubs.FindAsync(id);

    public async Task AddAsync(Club club)
    {
        _context.Clubs.Add(club);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Club club)
    {
        _context.Clubs.Update(club);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Club club)
    {
        _context.Clubs.Remove(club);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsOwnerAsync(int clubId, int userId)
    {
        return await _context.Clubs.AnyAsync(c => c.Id == clubId && c.CreatedByUserId == userId);
    }
}
