using Domain.Enums;

namespace Domain.Entities;

public class Question
{
    public int Id { get; set; }
    public int ExamId { get; set; }
    public string Text { get; set; }
    public QuestionType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}