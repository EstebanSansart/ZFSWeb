namespace API.Dtos;
public class UserDto
{
    // Properties
    public string UserCc { get; set; }
    public string Name { get; set; }
    public string Age { get; set; }
    public string Contact { get; set; }

    // Collections
    public ICollection<ReactionDto> Reactions { get; set; } = new HashSet<ReactionDto>();
    public ICollection<UserReactionDto> UserReactions { get; set; }
    public ICollection<EventDto> Events { get; set; } = new HashSet<EventDto>();
    public ICollection<EventAttendanceDto> EventAttendances { get; set; }
    public ICollection<TagDto> Tags { get; set; } = new HashSet<TagDto>();
    public ICollection<UserTagDto> UserTags { get; set; }

    // Foreign Keys
    public int CompanyId { get; set; }
    public int GenderId { get; set; }
    public int LevelId { get; set; }
}