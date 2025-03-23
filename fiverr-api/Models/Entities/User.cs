namespace fiverr_api.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string?  RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    
}