using fiverr_api.Models;
using fiverr_api.Models.DTOs;
using Microsoft.AspNetCore.Identity.Data;

namespace fiverr_api.Services.Interfaces;

public interface IAuthService
{
    Task<User?> RegisterAsync(UserDTO request);
    Task<TokenResponseDTO?> LoginAsync(UserDTO request);
    Task<TokenResponseDTO?> RefreshTokensAsync(RefreshTokenRequestDTO request);
    
}