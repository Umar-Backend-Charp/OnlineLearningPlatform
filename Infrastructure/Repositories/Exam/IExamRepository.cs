using Domain.Filter;

namespace Infrastructure.Repositories.Exam;

public interface IExamRepository
{
    Task<int> CreateExamAsync(Domain.Entities.Exam model);
    Task<int> UpdateExamAsync(Domain.Entities.Exam model);
    Task<int?> DeleteExamAsync(Guid examId);
    Task<List<Domain.Entities.Exam>?> GetExamsAsync(ExamFilter filter);
    Task<Domain.Entities.Exam?> GetExamByIdAsync(Guid examId);
}