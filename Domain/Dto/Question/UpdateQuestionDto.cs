using Domain.Enums;

namespace Domain.Dto.Question;

public class UpdateQuestionDto
{
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string CorrectAnswer { get; set; } = string.Empty;
}