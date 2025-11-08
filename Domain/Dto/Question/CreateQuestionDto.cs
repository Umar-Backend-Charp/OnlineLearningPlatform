using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Dto.Question;

public class CreateQuestionDto
{
    [Required]
    public required Guid ExamId { get; set; }
    [Required]
    public required string Text { get; set; }
    [Required]
    public required string CorrectAnswer { get; set; }
}