using aeroWebApi.Services;
using aeroWebApi.Repositories;
using aeroWebApi.Data;
using aeroWebApi.Entity;
using Microsoft.EntityFrameworkCore;
using aeroWebApi.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// register services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<PasswordHasher>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<BookingRepository>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<FlightRepository>();
builder.Services.AddScoped<FlightService>();


//db
builder.Services.AddDbContext<AeroDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("defaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


//  db with 2 passengers
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AeroDbContext>();
    // dbContext.Database.Migrate();

     try
    {
        if (dbContext.Database.CanConnect())
        {
            Console.WriteLine("✅ Database connection successful");
        }
        else
        {
            Console.WriteLine("❌ Cannot connect to database");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ DB connection error: {ex.Message}");
        // Log error if seeding fails
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the database.");
    }

    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();

    dbContext.Users.AddRange(
        new User
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "jdow@test.com",
            DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            PasswordHash = "testhash"
        },
        new User
        {
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jsmaith@mail.com",
            DateOfBirth = new DateTime(1985, 4, 2, 0, 0, 0, DateTimeKind.Utc),
            PasswordHash = "testhash"
        }
    );
    dbContext.SaveChanges();
}

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is NotFoundException)
        {
            context.Response.StatusCode = 404;
        }
        else
        {
            context.Response.StatusCode = 500;
        }

        await context.Response.WriteAsync(exception?.Message);
    });
});




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
