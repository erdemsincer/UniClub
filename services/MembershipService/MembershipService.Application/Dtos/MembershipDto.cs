namespace MembershipService.Application.Dtos;

public class MembershipDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ClubId { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime JoinedAt { get; set; }
}
