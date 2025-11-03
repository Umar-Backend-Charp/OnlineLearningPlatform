using Domain.Dto.Course;
using Infrastructure.Response;
using Infrastructure.Services.Course;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController(ICourseService service) : ControllerBase
{
    [HttpPost]
    public Task<Response<string>> CreateCourse(CreateCourseDto dto)
        => service.CreateCourse(dto);
    
    [HttpGet]
    public Task<Response<List<GetCourseDto>>>  GetCourses()
        => service.GetCourses();

    [HttpPut]
    public Task<Response<string>> UpdateCourse(UpdateCourseDto dto)
        => service.UpdateCourse(dto);

    [HttpDelete]
    public Task<Response<string>> DeleteCourse(int id)
        => service.DeleteCourse(id);

    [HttpGet("{id}")]
    public Task<Response<GetCourseDto>> GetCourse(int id)
        => service.GetCourseById(id);
}