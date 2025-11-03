using Domain.Dto.Exam;
using Infrastructure.Response;

namespace Infrastructure.Services.Exam;

public interface IExamService
{
    Task<Response<string>> CreateExam(CreateExamDto dto);
    Task<Response<string>> UpdateExam(UpdateExamDto dto);
    Task<Response<string>> DeleteExam(int id);
    Task<Response<List<GetExamDto>>> GetExams();
    Task<Response<GetExamDto>> GetExamById(int id);
}