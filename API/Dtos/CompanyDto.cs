namespace API.Dtos;
public class CompanyDto
{
    // Properties
    public int CompanyId { get; set; }
    public string Name { get; set; }
    public string Contact { get; set; }

    // Collections
    public ICollection<UserDto> Users { get; set; }
}