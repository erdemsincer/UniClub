using ClubService.Domain.Entities;

namespace ClubService.Application.Interfaces;

public interface IClubRepository
{
    Task<List<Club>> GetAllAsync();
    Task<Club?> GetByIdAsync(int id);
    Task AddAsync(Club club);
    Task UpdateAsync(Club club);
    Task DeleteAsync(Club club);
    Task<bool> IsOwnerAsync(int clubId, int userId);
}
