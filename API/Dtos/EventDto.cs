namespace API.Dtos;
public class EventDto
{
    // Properties
    public int EventId { get; set; }
    public string Name { get; set; }
    public string Capacity { get; set; }
    public string State { get; set; }
    public string EventPoints { get; set; }
    public DateTime Date { get; set; }
    public string Sponsorship { get; set; }

    // Collections
    public ICollection<UserDto> Users { get; set; } = new HashSet<UserDto>();
}