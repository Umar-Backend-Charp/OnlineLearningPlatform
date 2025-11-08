using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Dto.User;

public class CreateUserDto
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }
    [MaxLength(50)]
    public string LastName { get; set; }
    [Range(5, 99, ErrorMessage = "Age must be between 5 and 99.")]
    public int Age { get; set; }
    public string Phone { get; set; }
    [MaxLength(50)]
    public string Address { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    public Role Role { get; set; }
}