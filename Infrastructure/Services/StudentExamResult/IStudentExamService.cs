using Domain.Dto.StudentExamResult;
using Infrastructure.Response;

namespace Infrastructure.Services.StudentExamResult;

public interface IStudentExamService
{
    Task<Response<string>> CreateStudentExamResultAsync(CreateStudentExamResult dto);
    Task<Response<List<GetStudentExamResult>>> GetAllStudentExamResultAsync();
    Task<Response<GetStudentExamResult>> GetStudentExamResultByIdAsync(Guid id);
}