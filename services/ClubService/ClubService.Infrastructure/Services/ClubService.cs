using ClubService.Application.Dtos;
using ClubService.Application.Interfaces;
using ClubService.Domain.Entities;

namespace ClubService.Infrastructure.Services;

public class ClubService : IClubService
{
    private readonly IClubRepository _repo;

    public ClubService(IClubRepository repo)
    {
        _repo = repo;
    }

    public async Task<int> CreateAsync(CreateClubDto dto, int userId)
    {
        var club = new Club
        {
            Name = dto.Name,
            Description = dto.Description,
            LogoUrl = dto.LogoUrl,
            CreatedByUserId = userId
        };

        await _repo.AddAsync(club);
        return club.Id;
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var club = await _repo.GetByIdAsync(id);
        if (club == null || club.CreatedByUserId != userId) return false;

        await _repo.DeleteAsync(club);
        return true;
    }

    public async Task<List<ClubDto>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync();
        return list.Select(c => new ClubDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            LogoUrl = c.LogoUrl,
            CreatedByUserId = c.CreatedByUserId,
            CreatedAt = c.CreatedAt
        }).ToList();
    }

    public async Task<ClubDto?> GetByIdAsync(int id)
    {
        var c = await _repo.GetByIdAsync(id);
        if (c == null) return null;

        return new ClubDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            LogoUrl = c.LogoUrl,
            CreatedByUserId = c.CreatedByUserId,
            CreatedAt = c.CreatedAt
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateClubDto dto, int userId)
    {
        var club = await _repo.GetByIdAsync(id);
        if (club == null || club.CreatedByUserId != userId) return false;

        club.Name = dto.Name;
        club.Description = dto.Description;
        club.LogoUrl = dto.LogoUrl;

        await _repo.UpdateAsync(club);
        return true;
    }
}
