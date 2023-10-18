namespace Domain.Entities;
public class Level{
    // Properties
    public int LevelId { get; set; }
    public string LevelNumber { get; set; }
    public string CurrentPoints { get; set; }

    // Collections
    public ICollection<User> Users { get; set; }
}