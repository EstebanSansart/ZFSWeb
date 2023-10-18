namespace Domain.Entities;
public class EventAttendance{
    public string UserCc { get; set; }
    public User User { get; set; }
    public int EventId { get; set; }
    public Event Event { get; set; }
}