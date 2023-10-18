namespace Domain.Entities;
public class Tag{
    // Properties
    public int TagId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Collections
    public ICollection<User> Users { get; set; } = new HashSet<User>();
    public ICollection<UserTag> UserTags { get; set; }
}