using Domain.Dto.Lesson;
using Domain.Filter;
using Infrastructure.Response;
using Infrastructure.Services.Lesson;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonController(ILessonService service) : ControllerBase
{
    [Authorize(Roles = "Teacher, Admin")]
    [HttpPost]
    public Task<Response<string>> CreateLesson(CreateLessonDto dto)
        => service.CreateLessonAsync(dto);
    
    [Authorize]
    [HttpGet]
    public Task<Response<List<GetLessonDto>>> GetLessons([FromQuery] LessonFilter filter)
        => service.GetLessonsAsync(filter);
    
    [Authorize(Roles = "Teacher, Admin")]
    [HttpPut]
    public Task<Response<string>> UpdateLesson(UpdateLessonDto dto)
        => service.UpdateLessonAsync(dto);

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public Task<Response<string>> DeleteLesson(Guid id)
        => service.DeleteLessonAsync(id);
    
    [Authorize]
    [HttpGet("{id}")]
    public Task<Response<GetLessonDto>> GetLessonById(Guid id)
        => service.GetLessonByIdAsync(id);
}