using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser
{ 
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public override string? PhoneNumber { get; set; }
    public int Age { get; set; }
    public string Address { get; set; } = string.Empty;
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdateAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    
    public List<StudentCourse> StudentCourses { get; set; } = new();
}