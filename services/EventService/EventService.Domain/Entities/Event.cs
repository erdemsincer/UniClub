namespace EventService.Domain.Entities;

public class Event
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public int ClubId { get; set; }              // Hangi kulübe ait?
    public int CreatedByUserId { get; set; }     // JWT'den alınır

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
