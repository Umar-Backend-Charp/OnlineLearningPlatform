namespace Domain.Dto.StudentExamResult;

public class CreateStudentExamResult
{ 
    public Guid StudentId { get; set; }
    public Guid ExamId { get; set; }
    public int Score { get; set; }
}