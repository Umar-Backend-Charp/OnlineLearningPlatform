namespace Domain.Dto.Exam;

public class UpdateExamDto
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string Title { get; set; }
    public bool IsDeleted { get; set; }
}