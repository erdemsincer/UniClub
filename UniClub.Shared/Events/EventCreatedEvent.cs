namespace UniClub.Shared.Events;

public class EventCreatedEvent
{
    public int EventId { get; set; }
    public int ClubId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
}
