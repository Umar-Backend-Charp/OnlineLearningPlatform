using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Dto.Course;

public class CreateCourseDto
{
    [Required]
    [MaxLength(150)]
    public string Title { get; set; }
    public string? Description { get; set; }
    public Level Level { get; set; }
    [Required]
    public double Price { get; set; }
}