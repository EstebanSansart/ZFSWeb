namespace Domain.Entities;
public class Reaction{
    // Properties
    public int ReactionId { get; set; }
    public string Name { get; set; }

    // Collections
    public ICollection<User> Users { get; set; } = new HashSet<User>();
    public ICollection<UserReaction> UserReactions { get; set; }
}