using Domain.Enums;

namespace Domain.Dto.User;

public class GetUserDto
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
    public bool IsDeleted { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
}