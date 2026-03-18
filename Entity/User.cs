using System.ComponentModel.DataAnnotations;

namespace aeroWebApi.Entity;


public class User
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = "";
    [Required]
    [StringLength(50)]
    public string LastName { get; set; }= "";
    [Required]
    public DateTime DateOfBirth { get; set; }
    [Required]
    public string PasswordHash { get; set; } = "";
    [Required]
    [EmailAddress]
    
    public string Email { get; set; } = "";

// Domain methods
    public string FullName => $"{FirstName} {LastName}";

    public int CalculateAge()
    {
        var today = DateTime.Today;
        var age = today.Year - DateOfBirth.Year;
        if (DateOfBirth.Date > today.AddYears(-age))
            age--;
        return age;
    }

    public bool IsAdult()
        {
            return CalculateAge() >= 18;
        }
}   