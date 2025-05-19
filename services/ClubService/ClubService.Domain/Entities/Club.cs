namespace ClubService.Domain.Entities;

public class Club
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public int CreatedByUserId { get; set; }  // JWT içinden gelen UserId
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
