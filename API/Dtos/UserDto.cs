namespace API.Dtos;
public class UserDto
{
    // Properties
    public string UserCc { get; set; }
    public string Name { get; set; }
    public string Age { get; set; }
    public bool IsNew { get; set; }
    public string Contact { get; set; }
    public int LevelId { get; set; }
    public int Points { get; set; }
    // Foreign Keys
    public int CompanyId { get; set; }
    public int GenderId { get; set; }
   
}