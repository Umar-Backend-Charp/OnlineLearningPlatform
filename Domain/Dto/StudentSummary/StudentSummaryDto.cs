using Domain.Dto.Course;

namespace Domain.Dto.StudentSummary;

public class StudentSummaryDto
{
    public required string StudentId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public int Age { get; set; }
    public required string Email { get; set; }
    public List<GetCourseForSummaryDto> Courses { get; set; } = [];
}