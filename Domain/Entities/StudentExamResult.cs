namespace Domain.Entities;

public class StudentExamResult
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int ExamId { get; set; }
    public int Score { get; set; }
    public bool Passed  { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}