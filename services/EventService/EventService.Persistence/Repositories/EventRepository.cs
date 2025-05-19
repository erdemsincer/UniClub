using EventService.Application.Interfaces;
using EventService.Domain.Entities;
using EventService.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace EventService.Persistence.Repositories;

public class EventRepository : IEventRepository
{
    private readonly ApplicationDbContext _context;

    public EventRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Event>> GetAllAsync()
        => await _context.Events.ToListAsync();

    public async Task<List<Event>> GetByClubIdAsync(int clubId)
        => await _context.Events.Where(e => e.ClubId == clubId).ToListAsync();

    public async Task<Event?> GetByIdAsync(int id)
        => await _context.Events.FindAsync(id);

    public async Task AddAsync(Event ev)
    {
        _context.Events.Add(ev);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Event ev)
    {
        _context.Events.Update(ev);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Event ev)
    {
        _context.Events.Remove(ev);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsOwnerAsync(int eventId, int userId)
    {
        return await _context.Events.AnyAsync(e => e.Id == eventId && e.CreatedByUserId == userId);
    }
}
