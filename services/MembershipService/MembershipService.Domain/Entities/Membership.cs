namespace MembershipService.Domain.Entities;

public class Membership
{
    public int Id { get; set; }

    public int UserId { get; set; }         // AuthService'den
    public int ClubId { get; set; }         // ClubService'den

    public bool IsAdmin { get; set; } = false;
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
}
