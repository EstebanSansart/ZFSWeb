namespace Domain.Entities;
public class User{
    // Properties
    public string UserCc { get; set; }
    public string Name { get; set; }
    public string Age { get; set; }
    public string Contact { get; set; }
    public bool IsNew { get; set; }

    // Collections
    public ICollection<Reaction> Reactions { get; set; } = new HashSet<Reaction>();
    public ICollection<UserReaction> UserReactions { get; set; }
    public ICollection<Event> Events { get; set; } = new HashSet<Event>();
    public ICollection<EventAttendance> EventAttendances { get; set; }
    public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
    public ICollection<UserTag> UserTags { get; set; }
    public virtual ICollection<RefreshTokenRecord> RefreshTokenRecords { get; } = new List<RefreshTokenRecord>();

    // Foreign Keys
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public int GenderId { get; set; }
    public Gender Gender { get; set; }
    public int LevelId { get; set; }
    public Level Level { get; set; }
}