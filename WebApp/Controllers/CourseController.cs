using Domain.Dto.Course;
using Infrastructure.Response;
using Infrastructure.Services.Course;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController(ICourseService service) : ControllerBase
{
    [HttpPost]
    public async Task<Response<string>> CreateCourse(CreateCourseDto dto)
        => await service.CreateCourse(dto);
    
    [HttpGet]
    public async Task<Response<List<GetCourseDto>>>  GetCourses()
        => await service.GetCourses();

    [HttpPut]
    public async Task<Response<string>> UpdateCourse(UpdateCourseDto dto)
        => await service.UpdateCourse(dto);

    [HttpDelete]
    public async Task<Response<string>> DeleteCourse(string id)
        => await service.DeleteCourse(id);

    [HttpGet("{id}")]
    public async Task<Response<GetCourseDto>> GetCourse(string id)
        => await service.GetCourseById(id);

    [HttpPost("{courseId}/enroll/{studentId}")]
    public async Task<Response<string>> EnrollCourse([FromRoute] string courseId, [FromRoute] string studentId)
        => await service.EnrollStudent(courseId, studentId);
}