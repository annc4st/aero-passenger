using System.Text.RegularExpressions;
using aeroWebApi.DTOs;
using aeroWebApi.Repositories;
using aeroWebApi.Entity;


namespace aeroWebApi.Services;

public class PassengerService
{
    private readonly PassengerRepository _repository;
    private readonly PasswordHasher _passwordHasher;

    public PassengerService(PassengerRepository repository, PasswordHasher passwordHasher)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
    }


    public bool IsEmailValid(string email)
    {
        var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailPattern);
    }

    public async Task<PassengerResponseDto> CreatePassenger(CreatePassengerDto createDto)
    {
        if (!IsEmailValid(createDto.Email))
        {
            throw new ArgumentException("Invalid email format.");
        }

        if (await _repository.EmailExists(createDto.Email))
        {
            throw new InvalidOperationException("Passenger with this email already exists.");
        }

        //create passenger
        var passenger = new Passenger
        {
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Email = createDto.Email,
            DateOfBirth = createDto.DateOfBirth,
            PasswordHash = _passwordHasher.HashPassword(createDto.Password)
        };

        var createdPassenger = await _repository.Create(passenger);

        return new PassengerResponseDto
        {
            Id = createdPassenger.Id,
            FullName = createdPassenger.FullName,
            Email = createdPassenger.Email,
            Age = createdPassenger.CalculateAge()
        };
    }

    public async Task<PassengerResponseDto?> GetPassengerById(int id)
    {
        var passenger = await _repository.GetById(id);

        if (passenger == null)
            return null;

        return new PassengerResponseDto
        {
            Id = passenger.Id,
            FullName = passenger.FullName,
            Email = passenger.Email,
            Age = passenger.CalculateAge()
        };
    }

    public async Task<List<PassengerResponseDto>> GetAll()
    {
        var passengers = await _repository.GetAllPassengers();

        return passengers.Select(p => new PassengerResponseDto
        {
            Id = p.Id,
            FullName = p.FullName,
            Email = p.Email,
            Age = p.CalculateAge()
        }).ToList();
    }
}

