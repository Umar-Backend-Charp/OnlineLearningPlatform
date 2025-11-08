namespace Domain.Entities;

public class Answer
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid QuestionId { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}