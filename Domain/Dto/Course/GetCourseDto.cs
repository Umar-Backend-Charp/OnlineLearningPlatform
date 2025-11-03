using Domain.Enums;

namespace Domain.Dto.Course;

public class GetCourseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public Level Level { get; set; }
    public double Price { get; set; }
    public bool IsDeleted {get; set;}
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
}