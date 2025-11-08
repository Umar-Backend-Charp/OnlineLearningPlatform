using Domain.Dto.StudentExamResult;
using Infrastructure.Response;
using Infrastructure.Services.StudentExamResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentExamResultController(IStudentExamService serService) : ControllerBase
{
    [Authorize(Roles = "Teacher, Admin")]
    [HttpPost]
    public async Task<Response<string>> CreateStudentExamResult(CreateStudentExamResult dto)
        => await serService.CreateStudentExamResultAsync(dto);

    [Authorize(Roles = "Teacher, Admin")]
    [HttpGet]
    public async Task<Response<List<GetStudentExamResult>>> GetStudentExamResults()
        => await serService.GetAllStudentExamResultAsync();

    [Authorize(Roles = "Teacher, Admin")]
    [HttpGet("{id}")]
    public async Task<Response<GetStudentExamResult>> GetStudentExamResultById(Guid id)
        => await serService.GetStudentExamResultByIdAsync(id);
}