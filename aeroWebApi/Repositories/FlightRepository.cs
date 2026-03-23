using Microsoft.EntityFrameworkCore;
using aeroWebApi.Data;
using aeroWebApi.Entity;


namespace aeroWebApi.Repositories;

public class FlightRepository
{
    private readonly AeroDbContext _context;
    public FlightRepository(AeroDbContext context)
    {
        _context = context;
    }

    public async Task<Flight> CreateFlight(Flight flight)
    {
        _context.Flights.Add(flight);
        await _context.SaveChangesAsync();
        return flight;
    }

    public async Task<Flight?> GetFlightById(int flightId)
    {
        return await _context.Flights.FirstOrDefaultAsync(f => f.Id == flightId);
        
    }

    public async Task<IEnumerable<Flight>> GetFlights ()
    {
        var flights = await _context.Flights.ToListAsync();
        return flights;
    }

    //Get passengers on a particular flightId

    // public async Task<List<User>> GetPassengersFlight(int flightId)
    // {
        
    // }



}