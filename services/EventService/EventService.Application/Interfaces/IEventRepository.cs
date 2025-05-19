using EventService.Domain.Entities;

namespace EventService.Application.Interfaces;

public interface IEventRepository
{
    Task<List<Event>> GetAllAsync();
    Task<List<Event>> GetByClubIdAsync(int clubId);
    Task<Event?> GetByIdAsync(int id);
    Task AddAsync(Event ev);
    Task UpdateAsync(Event ev);
    Task DeleteAsync(Event ev);
    Task<bool> IsOwnerAsync(int eventId, int userId);
}
