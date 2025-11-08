using Domain.Dto.Question;
using Infrastructure.Response;

namespace Infrastructure.Services.Question;

public interface IQuestionService
{
    Task<Response<string>> CreateQuestionAsync(CreateQuestionDto question);
    Task<Response<string>> UpdateQuestionAsync(UpdateQuestionDto question);
    Task<Response<string>> DeleteQuestionAsync(Guid questionId);
    Task<Response<List<GetQuestionDto>>> GetQuestionsAsync(Guid examId);
    Task<Response<GetQuestionDto>> GetQuestionByIdAsync(Guid questionId);
}