using fiverr_api.Data;
using fiverr_api.Models;
using fiverr_api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fiverr_api.Repositories.Implementation;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    
    public UserRepository(ApplicationDbContext context) => _context = context;
    
    public Task<User?> GetUserByUsernameAsync(string username)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public Task<User?> GetUserById(Guid id)
    {
       return _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public Task AddUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UsernameExistsAsync(string username)
    {
        return _context.Users.AnyAsync(u => u.Username == username);
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}