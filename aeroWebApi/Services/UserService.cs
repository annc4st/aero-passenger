using System.Text.RegularExpressions;
using aeroWebApi.DTOs;
using aeroWebApi.Repositories;
using aeroWebApi.Entity;


namespace aeroWebApi.Services;

public class UserService
{
    private readonly UserRepository _repository;
    private readonly PasswordHasher _passwordHasher;

    public UserService(UserRepository repository, PasswordHasher passwordHasher)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
    }


    public bool IsEmailValid(string email)
    {
        var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailPattern);
    }

    private UserResponseDto MapUserToDto (User createdUser){

        return new UserResponseDto
        {
            Id = createdUser.Id,
            FullName = createdUser.FullName,
            Email = createdUser.Email,
            Age = createdUser.CalculateAge()
        };
    }

    public async Task<UserResponseDto> CreateUser(CreateUserDto createDto)
    {
        if (!IsEmailValid(createDto.Email))
        {
            throw new ArgumentException("Invalid email format.");
        }

        if (await _repository.EmailExists(createDto.Email))
        {
            throw new InvalidOperationException("Passenger with this email already exists.");
        }

        var user = new User
        {
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Email = createDto.Email,
            DateOfBirth = createDto.DateOfBirth,
            PasswordHash = _passwordHasher.HashPassword(createDto.Password)
        };

        var createdUser = await _repository.Create(user);
        return MapUserToDto(createdUser);
    }

    public async Task<UserResponseDto?> GetUserById(int id)
    {
        var user = await _repository.GetById(id);

        if (user == null)
            return null;

        return new UserResponseDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Age = user.CalculateAge()
        };
    }

    public async Task<List<UserResponseDto>> GetAll()
    {
        var users = await _repository.GetAllUsers();
        return users.Select(MapUserToDto).ToList();
    }
}

