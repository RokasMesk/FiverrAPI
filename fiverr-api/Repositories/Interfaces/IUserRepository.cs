using fiverr_api.Models;

namespace fiverr_api.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserById(Guid  id);
    Task AddUserAsync(User user);
    Task <bool> UsernameExistsAsync (string username);
    Task SaveChangesAsync();
}