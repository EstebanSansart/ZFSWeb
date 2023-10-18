namespace API.Dtos;
public class LevelDto
{
    // Properties
    public int LevelId { get; set; }
    public string LevelNumber { get; set; }
    public string CurrentPoints { get; set; }

    // Collections
    public ICollection<UserDto> Users { get; set; }
}