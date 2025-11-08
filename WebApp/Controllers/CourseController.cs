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
    [Authorize(Roles = "Teacher, Admin")]
    [HttpPost]
    public async Task<Response<string>> CreateCourse(CreateCourseDto dto)
        => await service.CreateCourse(dto);
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<Response<List<GetCourseDto>>>  GetCourses()
        => await service.GetCourses();

    [Authorize(Roles = "Teacher, Admin")]
    [HttpPut]
    public async Task<Response<string>> UpdateCourse(UpdateCourseDto dto)
        => await service.UpdateCourse(dto);

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<Response<string>> DeleteCourse(string id)
        => await service.DeleteCourse(id);

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<Response<GetCourseDto>> GetCourse(string id)
        => await service.GetCourseById(id);

    [Authorize(Roles = "Teacher, Admin")]
    [HttpPost("{courseId}/enroll/{studentId}")]
    public async Task<Response<string>> EnrollCourse([FromRoute] string courseId, [FromRoute] string studentId)
        => await service.EnrollStudent(courseId, studentId);
}