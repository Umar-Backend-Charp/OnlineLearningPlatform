using System.ComponentModel.DataAnnotations;

namespace Domain.Dto.UserAnswer;

public class CreateAnswerDto
{
    [Required]
    public required string UserId { get; set; }
    public Guid QuestionId { get; set; }
    public string Text { get; set; } = string.Empty;
}