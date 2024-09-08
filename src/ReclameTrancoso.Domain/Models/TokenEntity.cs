namespace Domain.Models;

public class TokenEntity : BaseEntity
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime? AccessTokenExpiresAt { get; set; }
    public DateTime? RefreshTokenExpiresAt { get; set; }
    public long? UserId { get; set; }
    public virtual User User { get; set; }

}