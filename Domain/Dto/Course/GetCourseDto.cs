using Domain.Dto.User;
using Domain.Enums;

namespace Domain.Dto.Course;

public class GetCourseDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public Level Level { get; set; }
    public double Price { get; set; }
    public bool IsDeleted {get; set;}
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
    
    public List<GetUserDto> Students { get; set; } = new List<GetUserDto>();
}