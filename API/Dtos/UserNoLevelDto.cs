namespace Api.Dtos;
public class UserNoLevelDto
{
    // Properties
    public string UserCc { get; set; }
    public string Name { get; set; }
    public string Age { get; set; }
    public bool IsNew { get; set; }
    public string Contact { get; set; }
   
  
    public string  Password { get; set; }
    // Foreign Keys
    public int CompanyId { get; set; }
    public int GenderId { get; set; }
   
}