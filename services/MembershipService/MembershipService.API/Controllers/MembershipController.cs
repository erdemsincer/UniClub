using MembershipService.Application.Dtos;
using MembershipService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MembershipService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembershipController : ControllerBase
{
    private readonly IMembershipService _service;

    public MembershipController(IMembershipService service)
    {
        _service = service;
    }

    private int GetUserId()
    {
        var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.Parse(idClaim!);
    }

    [Authorize]
    [HttpPost("join")]
    public async Task<IActionResult> Join([FromBody] JoinClubDto dto)
    {
        var userId = GetUserId();
        var result = await _service.JoinClubAsync(userId, dto.ClubId);
        return result ? Ok("Katıldın") : BadRequest("Zaten üyesin");
    }

    [Authorize]
    [HttpPost("leave")]
    public async Task<IActionResult> Leave([FromBody] JoinClubDto dto)
    {
        var userId = GetUserId();
        var result = await _service.LeaveClubAsync(userId, dto.ClubId);
        return result ? Ok("Ayrıldın") : NotFound("Zaten üye değilsin");
    }

    [Authorize]
    [HttpGet("mine")]
    public async Task<IActionResult> MyMemberships()
    {
        var userId = GetUserId();
        var list = await _service.GetMyMembershipsAsync(userId);
        return Ok(list);
    }

    [HttpGet("club/{clubId}")]
    public async Task<IActionResult> MembersByClub(int clubId)
    {
        var list = await _service.GetMembersByClubIdAsync(clubId);
        return Ok(list);
    }
}
