namespace Domain.Entities;
public class Gender{
    // Properties
    public int GenderId { get; set; }
    public string GenderType { get; set; }

    // Collections
    public ICollection<User> Users { get; set; }
}