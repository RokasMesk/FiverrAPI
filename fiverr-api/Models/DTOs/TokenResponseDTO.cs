namespace fiverr_api.Models.DTOs;

public class TokenResponseDTO
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}