using ClubService.Application.Dtos;
using ClubService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClubService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClubController : ControllerBase
{
    private readonly IClubService _service;

    public ClubController(IClubService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clubs = await _service.GetAllAsync();
        return Ok(clubs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var club = await _service.GetByIdAsync(id);
        return club == null ? NotFound() : Ok(club);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClubDto dto)
    {
        var userId = GetUserId();
        var id = await _service.CreateAsync(dto, userId);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateClubDto dto)
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

    private int GetUserId()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.Parse(userIdClaim!);
    }
}
