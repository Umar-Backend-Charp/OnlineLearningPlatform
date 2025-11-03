using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Dto.Auth;

public class Register
{
    [Required]
    [MaxLength(50)]
    public required string FirstName { get; set; }
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    [Range(1, 200, ErrorMessage = "Age must be between 1 and 200")]
    public int Age { get; set; }
    [MaxLength(50)]
    public string Address { get; set; } = string.Empty;
    
}