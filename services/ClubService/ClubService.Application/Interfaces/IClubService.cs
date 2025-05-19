using ClubService.Application.Dtos;

namespace ClubService.Application.Interfaces;

public interface IClubService
{
    Task<List<ClubDto>> GetAllAsync();
    Task<ClubDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreateClubDto dto, int userId);
    Task<bool> UpdateAsync(int id, UpdateClubDto dto, int userId);
    Task<bool> DeleteAsync(int id, int userId);
}
