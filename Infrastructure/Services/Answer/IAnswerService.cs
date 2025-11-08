using Domain.Dto.UserAnswer;
using Infrastructure.Response;

namespace Infrastructure.Services.Answer;

public interface IAnswerService
{
    Task<Response<string>> CreateAnswerAsync(CreateAnswerDto dto);
    Task<Response<string>> UpdateAnswerAsync(UpdateAnswerDto dto);
    Task<Response<string>> DeleteAnswerAsync(Guid id);
    Task<Response<List<GetAnswerDto>>> GetAnswersAsync(Guid questionId);
    Task<Response<GetAnswerDto>> GetAnswerByIdAsync(Guid answerId);
}