using Microsoft.EntityFrameworkCore;
using aeroWebApi.Data;
using aeroWebApi.Entity;

namespace aeroWebApi.Repositories;

public class PassengerRepository
{
    private readonly AeroDbContext _context;
    public PassengerRepository(AeroDbContext context)
    {
        _context = context;
    }

    public async Task<Passenger> Create(Passenger passenger)
    {
        _context.Passengers.Add(passenger);
        await _context.SaveChangesAsync();
        return passenger;
    }

    public async Task<Passenger?> GetPassengerByEmail(string email)
    {
        return await _context.Passengers.FirstOrDefaultAsync(p => p.Email == email);
    }

    public async Task<Passenger?> GetById(int id)
    {
        return await _context.Passengers.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Passenger>> GetAllPassengers()
    {
        return await _context.Passengers.ToListAsync();
    }

    // public bool EmailExists(string email)
    // {
    //     return _context.Passengers.Any(p => p.Email == email);
    // }

    public async Task<bool> EmailExists(string email)
    {
        return await _context.Passengers.AnyAsync(p => p.Email == email);
    }
}