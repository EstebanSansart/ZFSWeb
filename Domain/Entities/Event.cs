namespace Domain.Entities;
public class Event{
    // Properties
    public int EventId { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public bool State { get; set; }
    public int EventPoints { get; set; }
    public DateTime Date { get; set; }
    public string Sponsorship { get; set; }
    public ICollection<Image> Images { get; set; }

    // Collections
    public ICollection<User> Users { get; set; } = new HashSet<User>();
    public ICollection<EventAttendance> EventAttendances { get; set; }
}