namespace Domain.Entities;
public class UserReaction{
    public int UserCc { get; set; }
    public User User { get; set; }
    public int ReactionId { get; set; }
    public Reaction Reaction { get; set; }
}