namespace aeroWebApi.DTOs;

public class CreateFlightDto
{
    public string FlightNumber { get; set; } = "";
    public string Airline { get; set; } = "";
    public string Origin { get; set; } = "";
    public string Destination { get; set; } = "";
    public DateTime DepartureTime { get; set; } 
}