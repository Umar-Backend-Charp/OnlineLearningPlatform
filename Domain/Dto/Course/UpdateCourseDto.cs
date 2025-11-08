using Domain.Enums;

namespace Domain.Dto.Course;

public class UpdateCourseDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public Level Level { get; set; }
    public double Price { get; set; }
    public bool IsDeleted {get; set;}
}