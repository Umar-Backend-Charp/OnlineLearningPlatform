using System.Net;
using AutoMapper;
using Domain.Dto.StudentExamResult;
using Infrastructure.Repositories.StudentExamResult;
using Infrastructure.Response;

namespace Infrastructure.Services.StudentExamResult;

public class StudentExamResultService(IStudentExamResultRepository serRepository, IMapper mapper) : IStudentExamService
{
    public async Task<Response<string>> CreateStudentExamResultAsync(CreateStudentExamResult dto)
    {
        var mapped = mapper.Map<Domain.Entities.StudentExamResult>(dto);

        var result = await serRepository.CreateStudentExamResultAsync(mapped);

        return result > 0
            ? new Response<string>(HttpStatusCode.Created, "Student-Exam Result Created")
            : new Response<string>(HttpStatusCode.BadRequest, "Failed to create Student-Exam Result");
    }

    public async Task<Response<List<GetStudentExamResult>>> GetAllStudentExamResultAsync()
    {
        var results = await serRepository.GetAllStudentExamResultAsync();
        if (results.Count < 1) return new Response<List<GetStudentExamResult>>(HttpStatusCode.NotFound, "Student-Exam Result Not Found");
        
        var mappedResults = mapper.Map<List<GetStudentExamResult>>(results);
        return new Response<List<GetStudentExamResult>>(mappedResults);
    }

    public async Task<Response<GetStudentExamResult>> GetStudentExamResultByIdAsync(Guid id)
    {
        var ser = await serRepository.GetStudentExamResultByIdAsync(id);
        if (ser is null) return new Response<GetStudentExamResult>(HttpStatusCode.NotFound, "Student-Exam Result Not Found");
        
        var mapped = mapper.Map<GetStudentExamResult>(ser);
        
        return new Response<GetStudentExamResult>(mapped);
    }
}