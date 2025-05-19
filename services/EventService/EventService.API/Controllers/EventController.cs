using EventService.Application.Dtos;
using EventService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventService _service;

    public EventController(IEventService service)
    {
        _service = service;
    }

    private int GetUserId()
    {
        var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.Parse(idClaim!);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ev = await _service.GetByIdAsync(id);
        return ev == null ? NotFound() : Ok(ev);
    }

    [HttpGet("club/{clubId}")]
    public async Task<IActionResult> GetByClubId(int clubId)
    {
        var list = await _service.GetByClubIdAsync(clubId);
        return Ok(list);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEventDto dto)
    {
        var userId = GetUserId();
        var id = await _service.CreateAsync(dto, userId);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEventDto dto)
    {
        var userId = GetUserId();
        var success = await _service.UpdateAsync(id, dto, userId);
        return success ? NoContent() : Forbid();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = GetUserId();
        var success = await _service.DeleteAsync(id, userId);
        return success ? NoContent() : Forbid();
    }
}
