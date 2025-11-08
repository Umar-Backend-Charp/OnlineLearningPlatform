namespace Domain.Entities;

public class StudentCourse
{
    public string UserId { get; set; }
    public User? User { get; set; }

    public Guid CourseId { get; set; }
    public Course? Course { get; set; }

    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
}
