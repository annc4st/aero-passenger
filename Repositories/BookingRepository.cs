using Microsoft.EntityFrameworkCore;
using aeroWebApi.Data;
using aeroWebApi.Entity;
using aeroWebApi.Exceptions;

namespace aeroWebApi.Repositories;

public class BookingRepository
{
    private readonly AeroDbContext _context;
    public BookingRepository(AeroDbContext context)
    {
        _context = context;
    }

    public async Task<Booking> BookFlightAsync(int userId, int flightId)
    {// validate user exists

    var user = await _context.Users.FindAsync(userId);
    if (user == null)
    {
        throw new ArgumentException("User not found.");
    }

    // validate flight exists
    var flight = await _context.Flights.FindAsync(flightId);
    if (flight == null)    {
        throw new ArgumentException("Flight not found.");
    }
    // check for duplicates
    var existingBooking = await _context.Bookings
        .FirstOrDefaultAsync(b => b.PassengerId == userId && b.FlightId == flightId);
    if (existingBooking != null)    {
        throw new InvalidOperationException("Booking already exists for this user and flight.");
    }

    var booking = new Booking
    {
        PassengerId = userId,
        FlightId = flightId,
        BookingDate = DateTime.UtcNow
    };
    _context.Bookings.Add(booking);
    await _context.SaveChangesAsync();
    // return booking;

    return await _context.Bookings
        .Include(b => b.Passenger)
        .Include(b => b.Flight)
        .FirstAsync(b => b.Id == booking.Id);
    }

    public async Task<Booking?> GetById(int id)
    {
        return await _context.Bookings
            .Include(b => b.Passenger)
            .Include(b => b.Flight)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<List<Booking>> GetBookingsByEmail(string userEmail)
    {
        var userExists = await _context.Users
            .AnyAsync(u => u.Email == userEmail);

        if (!userExists)
        {
            throw new NotFoundException("User with the provided email does not exist.");
        }

        return await _context.Bookings
            .Include(b => b.Passenger)
            .Include(b => b.Flight) 
            .Where(b => b.Passenger.Email == userEmail)
            .ToListAsync();
    }
    
}

