using aeroWebApi.Entity;


namespace aeroWebApi.Data;

public class DbInitializer
{

    public static void Initialize(AeroDbContext context, ILogger logger)
    {
        try
        {
            // context.Database.Migrate();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


            if (!context.Database.CanConnect())
            {
                Console.WriteLine("✅ Database connection successful");
                logger.LogError("Cannot connect to database");
                return;
            }

            Seed(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ DB connection error: {ex.Message}");
            logger.LogError(ex, "Error during DB initialization");
        }
    }
    public static void Seed(AeroDbContext dbContext)
    {
        if (dbContext.Users.Any())
            return; // DB already seeded


        dbContext.Users.AddRange(
      new User
      {
          FirstName = "John",
          LastName = "Doe",
          Email = "jdow@test.com",
          DateOfBirth = new DateOnly(1987, 10, 1),
          PasswordHash = "testhash"
      },
      new User
      {
          FirstName = "Jane",
          LastName = "Smith",
          Email = "jsmith@mail.com",
          DateOfBirth = new DateOnly(2000, 10, 1),
          PasswordHash = "testhash"
      }
  );

        dbContext.Flights.AddRange(
            new Flight
            {
                FlightNumber = "RK123",
                Airline = "Lufthansa",
                Origin = "Berlin, Germany",
                Destination = "London, UK",
                DepartureTime = new DateTime(2026, 5, 1, 8, 30, 0, DateTimeKind.Utc)
            },
            new Flight
            {
                FlightNumber = "AI123",
                Airline = "Ryanair",
                Origin = "Dublin, Ireland",
                Destination = "Manchester, UK",
                DepartureTime = new DateTime(2026, 5, 2, 10, 15, 0, DateTimeKind.Utc)
            },
            new Flight
            {
                FlightNumber = "BW123",
                Airline = "EasyJet",
                Origin = "Glasgo, UK",
                Destination = "Barcelona, Spain",
                DepartureTime = new DateTime(2026, 5, 3, 16, 45, 0, DateTimeKind.Utc)
            }
        );

        dbContext.SaveChanges();
    }


}
