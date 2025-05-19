using EventService.Application.Dtos;

namespace EventService.Application.Interfaces;

public interface IEventService
{
    Task<List<EventDto>> GetAllAsync();
    Task<List<EventDto>> GetByClubIdAsync(int clubId);
    Task<EventDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreateEventDto dto, int userId);
    Task<bool> UpdateAsync(int id, UpdateEventDto dto, int userId);
    Task<bool> DeleteAsync(int id, int userId);
}
