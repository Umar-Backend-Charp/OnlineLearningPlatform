using Domain.Dto.Exam;
using Domain.Filter;
using Infrastructure.Response;

namespace Infrastructure.Services.Exam;

public interface IExamService
{
    Task<Response<string>> CreateExamAsync(CreateExamDto dto);
    Task<Response<string>> UpdateExamAsync(UpdateExamDto dto);
    Task<Response<string>> DeleteExamAsync(Guid id);
    Task<PaginationResponse<List<GetExamDto>>> GetExamsAsync(ExamFilter filter);
    Task<Response<GetExamDto>> GetExamByIdAsync(Guid id);
}