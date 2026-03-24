using aeroWebApi.DTOs;
// using aeroWebApi.Repositories;
using aeroWebApi.Entity;
using aeroWebApi.Exceptions;
using aeroWebApi.Repositories;

namespace aeroWebApi.Services;

public class FlightService
{
    private readonly IFlightRepository _repository;

    public FlightService(IFlightRepository repository)
    {
        _repository = repository;
    }

    private FlightResponseDto MapToDto(Flight createdFlight)
    {
        return new FlightResponseDto
        {
            Id = createdFlight.Id,
            FlightNumber = createdFlight.FlightNumber,
            Airline = createdFlight.Airline,
            Origin = createdFlight.Origin,
            Destination = createdFlight.Destination,
            DepartureTime = createdFlight.DepartureTime
        };
    }

    public async Task<FlightResponseDto> CreateFlight(CreateFlightDto createDto)
    {
        var flight = new Flight
        {
            FlightNumber = createDto.FlightNumber,
            Airline = createDto.Airline,
            Origin = createDto.Origin,
            Destination = createDto.Destination,
            DepartureTime = createDto.DepartureTime
        };

        var createdFlight = await _repository.CreateFlight(flight);
        return MapToDto(createdFlight);
    }

    public async Task<FlightResponseDto?> GetFlightById(int id)
    {
        var flight  = await _repository.GetFlightById(id);
        if (flight == null)
             throw new NotFoundException("Flight not found");

        return MapToDto(flight);
    }

    public async Task<List<FlightResponseDto>> GetFlightsService()
    {
        var flights = await _repository.GetFlights();
        return flights.Select(MapToDto).ToList();
    }
}
