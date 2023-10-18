namespace API.Dtos;
public class UserDto
{
    // Properties
    public int UserCc { get; set; }
    public string Name { get; set; }
    public string Age { get; set; }
    public string Contact { get; set; }

    // Foreign Keys
    public int CompanyId { get; set; }
    public int GenderId { get; set; }
    public int LevelId { get; set; }
}