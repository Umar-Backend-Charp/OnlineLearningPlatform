using Domain.Dto.Lesson;
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
        => service.CreateLesson(dto);
    
    [HttpGet]
    public Task<Response<List<GetLessonDto>>> GetLessons()
        => service.GetLessons();
    
    [HttpPut]
    public Task<Response<string>> UpdateLesson(UpdateLessonDto dto)
        => service.UpdateLesson(dto);

    [HttpDelete]
    public Task<Response<string>> DeleteLesson(int id)
        => service.DeleteLesson(id);
    
    [HttpGet("{id}")]
    public Task<Response<GetLessonDto>> GetLessonById(int id)
        => service.GetLessonById(id);
}