using Domain.Dto.Question;

namespace Infrastructure.Repositories.Question;

public interface IQuestionRepository
{
    Task<int> CreateQuestionAsync(Domain.Entities.Question question);
    Task<int> UpdateQuestionAsync(Domain.Entities.Question question);
    Task<int?> DeleteQuestionAsync(Guid questionId);
    Task<List<Domain.Entities.Question>> GetQuestionsAsync(Guid examId);
    Task<Domain.Entities.Question?> GetQuestionByIdAsync(Guid questionId);
}