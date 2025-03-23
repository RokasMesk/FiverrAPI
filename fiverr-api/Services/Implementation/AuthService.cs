
using fiverr_api.Models;
using fiverr_api.Models.DTOs;
using fiverr_api.Repositories.Interfaces;
using fiverr_api.Services.Interfaces;

namespace fiverr_api.Services.Implementation;

public class AuthService(IUserRepository repo, ITokenService tokenService) : IAuthService
{
    public async Task<TokenResponseDTO?> LoginAsync(UserDTO request)
    {
        var user = await repo.GetUserByUsernameAsync(request.Username);
        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return null;
        }

        var response = new TokenResponseDTO {
            AccessToken = tokenService.GenerateAccessToken(user),
            RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
        };
        return response;

    }

    public async Task<User?> RegisterAsync(UserDTO request)
    {
        if (await repo.UsernameExistsAsync(request.Username))
        {
            return null;
        }

        var user = new User();
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
       
        user.Username = request.Username;
        user.PasswordHash = hashedPassword;
        await repo.AddUserAsync(user);
        await repo.SaveChangesAsync();
        return user;
    }

    public async Task<TokenResponseDTO?> RefreshTokensAsync(RefreshTokenRequestDTO request)
    {
        var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
        if (user is null)
        {
            return null;
        }
        var response = new TokenResponseDTO {
            AccessToken = tokenService.GenerateAccessToken(user),
            RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
        };
        return response;
    }

   

    private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
    {
        var refreshToken = tokenService.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        await repo.SaveChangesAsync();
        return refreshToken;
    }
    
    private async Task<User> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
    {
        var user = await repo.GetUserById(userId);
        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiry < DateTime.UtcNow)
        {
            return null;
        }

        return user;
    }
    
}