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

// Transient: A new instance is created every time it is requested.
// Scoped: A new instance is created once per request or scope.
// Singleton: A single instance is created and shared throughout the application’s lifetime.

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

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AeroDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    DbInitializer.Initialize(dbContext, logger);
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
