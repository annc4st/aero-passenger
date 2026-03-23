using System.ComponentModel.DataAnnotations;

namespace aeroWebApi.DTOs;

public class CreateFlightDto
{
    [Required]
    public string FlightNumber { get; set; } = "";
    [Required]
    public string Airline { get; set; } = "";
    [Required]
    public string Origin { get; set; } = "";
    [Required]
    public string Destination { get; set; } = "";
    [Required] 
    public DateTime DepartureTime { get; set; }
}