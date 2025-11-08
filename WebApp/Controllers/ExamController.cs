using Domain.Dto.Exam;
using Domain.Filter;
using Infrastructure.Response;
using Infrastructure.Services.Exam;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExamController(IExamService service) : ControllerBase
{
    [Authorize(Roles = "Teacher, Admin")]
    [HttpPost]
    public Task<Response<string>> CreateExam(CreateExamDto dto)
        => service.CreateExamAsync(dto);
    
    [Authorize(Roles = "Teacher, Admin")]
    [HttpGet]
    public Task<PaginationResponse<List<GetExamDto>>> GetExams([FromQuery] ExamFilter filter)
        => service.GetExamsAsync(filter);
    
    [Authorize(Roles = "Teacher, Admin")]
    [HttpPut]
    public Task<Response<string>> UpdateExam(UpdateExamDto dto)
        => service.UpdateExamAsync(dto);

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public Task<Response<string>> DeleteExam(Guid id)
        => service.DeleteExamAsync(id);
    
    [Authorize]
    [HttpGet("{id}")]
    public Task<Response<GetExamDto>> GetExamById(Guid id)
        => service.GetExamByIdAsync(id);
}