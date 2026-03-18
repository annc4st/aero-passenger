namespace aeroWebApi.Entity;

public class Booking
{
    public int Id { get; set; }
    public int PassengerId { get; set; }
    public User Passenger { get; set; } = null!;
    public int FlightId { get; set; }
    
    public DateTime BookingDate { get; set; }
    // public string SeatNumber { get; set; } = "";
    public Flight Flight { get; set; } = null!;
}