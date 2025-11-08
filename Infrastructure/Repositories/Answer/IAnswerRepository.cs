using Domain.Dto.UserAnswer;

namespace Infrastructure.Repositories.Answer;

public interface IAnswerRepository
{
    Task<int> CreateAnswerAsync(Domain.Entities.Answer answer);
    Task<int> UpdateAnswerAsync(Domain.Entities.Answer answer);
    Task<int?> DeleteAnswerAsync(Guid id);
    Task<List<Domain.Entities.Answer>> GetAllAnswersAsync(Guid questionId);
    Task<Domain.Entities.Answer?> GetAnswerByIdAsync(Guid userId);
}