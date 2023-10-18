namespace API.Dtos;
public class GenderDto
{
    // Properties
    public int GenderId { get; set; }
    public string GenderType { get; set; }

    // Collections
    public ICollection<UserDto> Users { get; set; }
}