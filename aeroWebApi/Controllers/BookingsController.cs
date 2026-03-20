using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OpenApi;
using aeroWebApi.Services;
using aeroWebApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace aeroWebApi.Controllers;

// POST   /api/bookings
// GET    /api/bookings?userId=1
// GET    /api/bookings?flightId=10
// GET    /api/bookings/{id}

[ApiController]
[Route("api/[controller]")]

public class BookingsController: ControllerBase
{
    private readonly BookingService _service;

    public BookingsController (BookingService service)
    {
        _service = service;
    }

// GET /api/bookings?userId=1&flightId=10
    [HttpGet]
     public async Task<ActionResult<BookingResponseDto>> GetAllBookings([FromQuery] int? userId, [FromQuery] int? flightId)
{
    var bookings = await _service.FetchBookings(userId, flightId);

    return Ok(bookings);
}

    [HttpGet("{id}")]
  public async Task<ActionResult<BookingResponseDto>> GetBookingById(int id)
{
    var booking = await _service.GetBookingById(id);

    return Ok(booking);
}

    // GET /api/bookings/by-email?email=user@example.com
    [HttpGet("by-email")]
    public async Task<ActionResult<BookingResponseDto>> GetBookingsByUserEmail([FromQuery] string email)
    {
        var bookings = await _service.ListBookingsByEmail(email);
        return Ok(bookings);
    }


    [HttpPost]
    public async Task<ActionResult<BookingResponseDto>> PostBooking([FromBody] CreateBookingDto dto)
    {
        var createdBooking = await _service.CreateBooking(dto);
        return CreatedAtAction(nameof(GetBookingById),
        new { id = createdBooking.Id},
        createdBooking
        );
    }



}

