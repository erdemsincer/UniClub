namespace EventService.Application.Dtos;

public class CreateEventDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int ClubId { get; set; }
}
