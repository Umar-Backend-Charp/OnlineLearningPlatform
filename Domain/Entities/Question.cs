using Domain.Enums;

namespace Domain.Entities;

public class Question
{
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public string Text { get; set; } = string.Empty;
    public required string CorrectAnswer { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}