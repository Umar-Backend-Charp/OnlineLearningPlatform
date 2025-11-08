using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Domain.Entities;

public class Lesson
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Order { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdateAt { get; set; }
    
    public Course Course { get; set; }
}