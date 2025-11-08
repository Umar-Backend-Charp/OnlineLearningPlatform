using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Exam
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string Title { get; set; } = String.Empty;
    public int MaxScore { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdateAt { get; set; }
    public bool IsDeleted { get; set; }
    
    public List<Question> Questions { get; set; } = new List<Question>();
}