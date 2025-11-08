namespace Domain.Dto.UserAnswer;

public class GetAnswerDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid QuestionId { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}