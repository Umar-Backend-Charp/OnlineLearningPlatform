namespace Domain.Dto.Exam;

public class CreateExamDto
{
    public int CourseId { get; set; }
    public string Title { get; set; }
    public int MaxScore { get; set; }
}