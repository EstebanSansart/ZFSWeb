namespace API.Dtos;
public class UserDto
{
    // Properties
    public int UserCc { get; set; }
    public string Name { get; set; }
    public string Age { get; set; }
    public string Contact { get; set; }

    // Collections
    public ICollection<ReactionDto> Reactions { get; set; } = new HashSet<ReactionDto>();
    public ICollection<EventDto> Events { get; set; } = new HashSet<EventDto>();
    public ICollection<TagDto> Tags { get; set; } = new HashSet<TagDto>();

    // Foreign Keys
    public int CompanyId { get; set; }
    public int GenderId { get; set; }
    public int LevelId { get; set; }
}