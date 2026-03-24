using Xunit;
using Moq;
using aeroWebApi.Services;
using aeroWebApi.Repositories;
using aeroWebApi.DTOs;
using aeroWebApi.Entity;
using aeroWebApi.Exceptions;

namespace aeroWebApi.Test.Services;

public class FlightServiceTests
{
    private readonly Mock<IFlightRepository> _mockRepo;
    private readonly FlightService _service;

    public FlightServiceTests()
    {
        _mockRepo = new Mock<IFlightRepository>(); // crates fake reposit
        _service = new FlightService(_mockRepo.Object); //actual object passed to service
    }

    [Fact]
    public async Task CreateFlight_ShouldRetunFLightResponseDto()
    {
        //arrange
        var dto = new CreateFlightDto
        {
            FlightNumber = "RK123",
            Airline = "TestAir",
            Origin = "A",
            Destination = "B",
            DepartureTime = DateTime.UtcNow
        };

        var flight = new Flight
        {
            Id = 1,
            FlightNumber = dto.FlightNumber,
            Airline = dto.Airline,
            Origin = dto.Origin,
            Destination = dto.Destination,
            // Destination = "D", //to fail test
            DepartureTime = dto.DepartureTime
        };

        _mockRepo
            .Setup(r => r.CreateFlight(It.IsAny<Flight>()))
            .ReturnsAsync(flight); //return this value instead of calling DB

        // act
        var result = await _service.CreateFlight(dto);

        // assert
        Assert.NotNull(result);
        Assert.Equal(dto.FlightNumber, result.FlightNumber);
        Assert.Equal(dto.Destination, flight.Destination);
        Assert.Equal(1, result.Id);

    }

    [Fact]
    public async Task GetFlightById_WhenNotFound_ShouldThrowException ()
    {
        _mockRepo.Setup(r => r.GetFlightById(9))
        .ReturnsAsync((Flight?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetFlightById(9));
    }


}