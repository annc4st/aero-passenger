using Microsoft.EntityFrameworkCore;
using aeroWebApi.Data;
using aeroWebApi.Entity;

namespace aeroWebApi.Repositories;

public class UserRepository
{
    private readonly AeroDbContext _context;
    public UserRepository(AeroDbContext context)
    {
        _context = context;
    }

    public async Task<User> Create(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetById(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }


    public async Task<bool> EmailExists(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }
}