namespace Domain.Dto.Lesson;

public class UpdateLessonDto
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Order { get; set; }
    public bool IsDeleted { get; set; }
}