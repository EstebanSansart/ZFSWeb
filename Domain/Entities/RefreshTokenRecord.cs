namespace Domain.Entities;

public partial class RefreshTokenRecord
{
    // Properties
    public int RefreshTokenRecordId { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; }

    // Foreign Keys
    public string UserCc { get; set; }
    public virtual User User { get; set; }
}