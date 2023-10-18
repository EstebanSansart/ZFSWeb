namespace API.Dtos;
public class TagDto
{
    // Properties
    public int TagId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Collections
    public ICollection<UserDto> Users { get; set; } = new HashSet<UserDto>();
}