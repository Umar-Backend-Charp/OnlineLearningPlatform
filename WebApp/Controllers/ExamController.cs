using Domain.Dto.Exam;
using Domain.Filter;
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
        => service.CreateExamAsync(dto);
    
    [HttpGet]
    public Task<PaginationResponse<List<GetExamDto>>> GetExams([FromQuery] ExamFilter filter)
        => service.GetExamsAsync(filter);
    
    [HttpPut]
    public Task<Response<string>> UpdateExam(UpdateExamDto dto)
        => service.UpdateExamAsync(dto);

    [HttpDelete]
    public Task<Response<string>> DeleteExam(Guid id)
        => service.DeleteExamAsync(id);
    
    [HttpGet("{id}")]
    public Task<Response<GetExamDto>> GetExamById(Guid id)
        => service.GetExamByIdAsync(id);
}