namespace Domain.Dto.Lesson;

public class UpdateLessonDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Order { get; set; }
    public bool IsDeleted { get; set; }
}