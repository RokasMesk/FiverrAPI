using fiverr_api.Models;
using Microsoft.EntityFrameworkCore;

namespace fiverr_api.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}