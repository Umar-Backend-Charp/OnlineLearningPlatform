namespace Domain.Dto.Lesson;

public class CreateLessonDto
{
    public Guid CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}