using aeroWebApi.Services;
using aeroWebApi.DTOs;
using Microsoft.AspNetCore.Mvc;


namespace aeroWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class FlightsController: ControllerBase
{
    private readonly FlightService _service;

    public FlightsController (FlightService service)
    {
        _service = service;
    }

    [HttpGet]

    public async Task<ActionResult<FlightResponseDto>> GetFlights()
{
    var flights = await _service.GetFlightsService();

    return Ok(flights);
}

    [HttpGet("{id}")]
public async Task<ActionResult<FlightResponseDto>> GetFlightById(int id)
{
    var flight = await _service.GetFlightById(id);

    return Ok(flight);
}


    [HttpPost]
    public async Task<IActionResult> PostFLight([FromBody] CreateFlightDto createDto)
    {

         var createdFlight = await _service.CreateFlight(createDto);

       return CreatedAtAction(
        nameof(GetFlightById),
        new { id = createdFlight.Id },
        createdFlight
    );
    }
}