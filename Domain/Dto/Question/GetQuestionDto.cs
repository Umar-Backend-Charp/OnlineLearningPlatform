using Domain.Enums;

namespace Domain.Dto.Question;

public class GetQuestionDto
{
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string CorrectAnswer { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}