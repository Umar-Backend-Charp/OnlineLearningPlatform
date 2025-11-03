namespace Domain.Dto.Exam;

public class GetExamDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string Title { get; set; }
    public int MaxScore { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
}