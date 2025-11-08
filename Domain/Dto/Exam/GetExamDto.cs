using Domain.Dto.Question;

namespace Domain.Dto.Exam;

public class GetExamDto
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string Title { get; set; }
    public int MaxScore { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
    
    public List<GetQuestionDto> Questions { get; set; } = new List<GetQuestionDto>();
}