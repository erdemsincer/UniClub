using EventService.Application.Dtos;
using EventService.Application.Interfaces;
using EventService.Domain.Entities;
using MassTransit;
using UniClub.Shared.Events;

namespace EventService.Infrastructure.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _repo;
    private readonly IPublishEndpoint _publishEndpoint;



    public EventService(IEventRepository repo, IPublishEndpoint publishEndpoint)
    {
        _repo = repo;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<int> CreateAsync(CreateEventDto dto, int userId)
    {
        var ev = new Domain.Entities.Event
        {
            Title = dto.Title,
            Description = dto.Description,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            ClubId = dto.ClubId,
            CreatedByUserId = userId
        };

        await _repo.AddAsync(ev);

        var eventCreated = new EventCreatedEvent
        {
            EventId = ev.Id,
            ClubId = ev.ClubId,
            Title = ev.Title,
            Description = ev.Description,
            StartTime = ev.StartTime
        };

        await _publishEndpoint.Publish(eventCreated);

        return ev.Id;
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var ev = await _repo.GetByIdAsync(id);
        if (ev == null || ev.CreatedByUserId != userId)
            return false;

        await _repo.DeleteAsync(ev);
        return true;
    }

    public async Task<List<EventDto>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync();
        return list.Select(e => new EventDto
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            StartTime = e.StartTime,
            EndTime = e.EndTime,
            ClubId = e.ClubId,
            CreatedByUserId = e.CreatedByUserId,
            CreatedAt = e.CreatedAt
        }).ToList();
    }

    public async Task<List<EventDto>> GetByClubIdAsync(int clubId)
    {
        var list = await _repo.GetByClubIdAsync(clubId);
        return list.Select(e => new EventDto
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            StartTime = e.StartTime,
            EndTime = e.EndTime,
            ClubId = e.ClubId,
            CreatedByUserId = e.CreatedByUserId,
            CreatedAt = e.CreatedAt
        }).ToList();
    }

    public async Task<EventDto?> GetByIdAsync(int id)
    {
        var e = await _repo.GetByIdAsync(id);
        if (e == null) return null;

        return new EventDto
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            StartTime = e.StartTime,
            EndTime = e.EndTime,
            ClubId = e.ClubId,
            CreatedByUserId = e.CreatedByUserId,
            CreatedAt = e.CreatedAt
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateEventDto dto, int userId)
    {
        var ev = await _repo.GetByIdAsync(id);
        if (ev == null || ev.CreatedByUserId != userId)
            return false;

        ev.Title = dto.Title;
        ev.Description = dto.Description;
        ev.StartTime = dto.StartTime;
        ev.EndTime = dto.EndTime;

        await _repo.UpdateAsync(ev);
        return true;
    }
}
