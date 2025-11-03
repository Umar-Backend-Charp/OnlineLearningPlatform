using Domain.Dto.Course;
using Infrastructure.Data;
using Infrastructure.Response;

namespace Infrastructure.Services.Course;

public interface ICourseService
{
    Task<Response<string>> CreateCourse(CreateCourseDto dto);
    Task<Response<string>> UpdateCourse(UpdateCourseDto dto);
    Task<Response<string>> DeleteCourse(int id);
    Task<Response<List<GetCourseDto>>> GetCourses();
    Task<Response<GetCourseDto>> GetCourseById(int id);
}