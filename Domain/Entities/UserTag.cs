namespace Domain.Entities;
public class UserTag{
    public string UserCc { get; set; }
    public User User { get; set; }
    public int TagId { get; set; }
    public Tag Tag { get; set; }
}