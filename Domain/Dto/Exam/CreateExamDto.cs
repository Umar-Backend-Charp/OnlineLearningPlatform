namespace Domain.Dto.Exam;

public class CreateExamDto
{
    public Guid CourseId { get; set; }
    public string Title { get; set; }
}