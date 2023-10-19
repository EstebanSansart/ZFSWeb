namespace Domain.Entities;
public class Level{
    // Properties
    public int LevelId { get; set; }
    public int LevelNumber { get; set; }
    public int CurrentPoints { get; set; }

    // Collections
    public ICollection<User> Users { get; set; }
}