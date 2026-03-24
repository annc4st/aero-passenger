using aeroWebApi.Entity;

namespace aeroWebApi.Repositories;

public interface IFlightRepository
{
    Task<Flight> CreateFlight(Flight flight);
    Task<Flight?> GetFlightById(int flightId);
    Task<IEnumerable<Flight>> GetFlights();
}
