using Domain.Dto.Exam;
using Infrastructure.Response;
using Infrastructure.Services.Exam;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExamController(IExamService service) : ControllerBase
{
    [HttpPost]
    public Task<Response<string>> CreateExam(CreateExamDto dto)
        => service.CreateExam(dto);
    
    [HttpGet]
    public Task<Response<List<GetExamDto>>> GetExams()
        => service.GetExams();
    
    [HttpPut]
    public Task<Response<string>> UpdateExam(UpdateExamDto dto)
        => service.UpdateExam(dto);

    [HttpDelete]
    public Task<Response<string>> DeleteExam(int id)
        => service.DeleteExam(id);
    
    [HttpGet("{id}")]
    public Task<Response<GetExamDto>> GetExamById(int id)
        => service.GetExamById(id);
}