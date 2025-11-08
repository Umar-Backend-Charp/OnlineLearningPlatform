namespace Domain.Entities;

public class StudentExamResult
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid ExamId { get; set; }
    public int Score { get; set; }
    public bool Passed  { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}