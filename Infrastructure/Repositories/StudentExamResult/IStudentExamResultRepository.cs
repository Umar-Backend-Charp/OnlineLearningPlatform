using Domain.Dto.StudentExamResult;

namespace Infrastructure.Repositories.StudentExamResult;

public interface IStudentExamResultRepository
{
    Task<int> CreateStudentExamResultAsync(Domain.Entities.StudentExamResult studentExamResult);
    Task<Domain.Entities.StudentExamResult?> GetStudentExamResultByIdAsync(Guid id);
    Task<List<Domain.Entities.StudentExamResult>> GetAllStudentExamResultAsync();
}