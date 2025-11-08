using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class Course
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public Level Level { get; set; }
    public double Price { get; set; }
    public bool IsDeleted {get; set;} = false;
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdateAt { get; set; }
    
    public List<StudentCourse> StudentCourses { get; set; } = new();
    public List<Lesson> Lessons { get; set; } = new List<Lesson>();
    public List<Exam> Exams { get; set; } = new List<Exam>();
}