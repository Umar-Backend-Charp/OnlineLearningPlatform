namespace Domain.Filter;

public class LessonFilter : BaseFilter
{
    public Guid? CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
}