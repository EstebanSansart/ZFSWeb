namespace API.Dtos;
public class ReactionDto
{
    // Properties
    public int ReactionId { get; set; }
    public string Name { get; set; }

    // Collections
    public ICollection<UserDto> Users { get; set; } = new HashSet<UserDto>();
}