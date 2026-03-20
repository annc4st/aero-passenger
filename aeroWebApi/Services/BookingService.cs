using aeroWebApi.DTOs;
using aeroWebApi.Repositories;
using aeroWebApi.Entity;
using aeroWebApi.Exceptions;

namespace aeroWebApi.Services;

public class BookingService
{
    private readonly BookingRepository _repository;

    public BookingService(BookingRepository repository)
    {
        _repository = repository;
    }

    // mapping helper function
    private BookingResponseDto MapToDto(Booking booking)
    {
        return new BookingResponseDto
        {
            Id = booking.Id,
            PassengerName = booking.Passenger.FullName,
            FlightNumber = booking.Flight.FlightNumber,
            Airline = booking.Flight.Airline,
            Origin = booking.Flight.Origin,
            Destination = booking.Flight.Destination,
            DepartureTime = booking.Flight.DepartureTime
        };
    }

    //creates booking but returns dto
    public async Task<BookingResponseDto> CreateBooking(CreateBookingDto createDto)
    {
        var createdBooking = await _repository.CreateBookingAsync(createDto.PassengerId, createDto.FlightId);

        return MapToDto(createdBooking);
    }

    public async Task<BookingResponseDto?> GetBookingById(int id)
    {
        var booking = await _repository.GetById(id);

        if (booking == null)
        {
            Console.WriteLine("Booking null");
           throw new NotFoundException("Booking not found");
        }

        return MapToDto(booking);
    }

    public async Task<List<BookingResponseDto>> FetchBookings(int? userId, int? flightId)
    {
        var bookings = await _repository.GetBookings(userId, flightId);

        return bookings.Select(MapToDto).ToList();
    }

    //list of bookings for useremail
    public async Task<List<BookingResponseDto>> ListBookingsByEmail(string userEmail)
    {
        var bookings = await _repository.GetBookingsByEmail(userEmail);

        return bookings.Select(MapToDto).ToList();
    }

}