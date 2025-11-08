namespace Domain.Dto.UserAnswer;

public class UpdateAnswerDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid QuestionId { get; set; }
    public string Text { get; set; } = string.Empty;
}