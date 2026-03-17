using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OpenApi; // not mvc
using aeroWebApi.Services;
using aeroWebApi.DTOs;
using Microsoft.AspNetCore.Mvc;


namespace aeroWebApi.Controllers;

// GET /api/passengers
// POST /api/passengers
// GET /api/passengers/{id}
// DELETE /api/passengers/{id}

[ApiController]
[Route("api/[controller]")]
public class PassengerController : ControllerBase
{
    private readonly PassengerService _service;
    public PassengerController(PassengerService service)
    {
        _service = service;
    }

    // GET /api/passenger
    [HttpGet]
    public async Task<IActionResult> GetPassengers()
    {
        var passengers = await _service.GetAll();
        return Ok(passengers);
    }
    // GET /api/passengers/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPassengerById(int id)
    {
        var passenger = await _service.GetPassengerById(id);
        if (passenger == null)
            return NotFound();

        return Ok(passenger);
    }


    [HttpPost]
    public async Task<IActionResult> PostPassenger([FromBody] CreatePassengerDto passengerDto)
    {
        try
        {
            var createdPassenger = await _service.CreatePassenger(passengerDto);

            return CreatedAtAction(
                nameof(GetPassengerById),
                new { id = createdPassenger.Id },
                createdPassenger
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}




