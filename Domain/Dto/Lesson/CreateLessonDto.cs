namespace Domain.Dto.Lesson;

public class CreateLessonDto
{
    public int CourseId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Order { get; set; }
}