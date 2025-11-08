using Domain.Dto.Lesson;
using Domain.Filter;
using Infrastructure.Response;
using Infrastructure.Services.Lesson;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonController(ILessonService service) : ControllerBase
{
    [HttpPost]
    public Task<Response<string>> CreateLesson(CreateLessonDto dto)
        => service.CreateLessonAsync(dto);
    
    [HttpGet]
    public Task<Response<List<GetLessonDto>>> GetLessons([FromQuery] LessonFilter filter)
        => service.GetLessonsAsync(filter);
    
    [HttpPut]
    public Task<Response<string>> UpdateLesson(UpdateLessonDto dto)
        => service.UpdateLessonAsync(dto);

    [HttpDelete]
    public Task<Response<string>> DeleteLesson(Guid id)
        => service.DeleteLessonAsync(id);
    
    [HttpGet("{id}")]
    public Task<Response<GetLessonDto>> GetLessonById(Guid id)
        => service.GetLessonByIdAsync(id);
}