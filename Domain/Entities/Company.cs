namespace Domain.Entities;
public class Company{
    // Properties
    public int CompanyId { get; set; }
    public string Name { get; set; }
    public string Contact { get; set; }

    // Collections
    public ICollection<User> Users { get; set; }
}