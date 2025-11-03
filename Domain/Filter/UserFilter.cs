using Domain.Enums;

namespace Domain.Filter;

public class UserFilter : BaseFilter
{
    public string? FirstName { get; set; }
    public int? Age { get; set; }
    public string? Email { get; set; }
    public Role? Role { get; set; }
}