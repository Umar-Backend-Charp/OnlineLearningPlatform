using Domain.Dto.Lesson;
using Infrastructure.Response;

namespace Infrastructure.Services.Lesson;

public interface ILessonService
{
    Task<Response<string>> CreateLesson(CreateLessonDto dto);
    Task<Response<string>> UpdateLesson(UpdateLessonDto dto);
    Task<Response<string>> DeleteLesson(int id);
    Task<Response<List<GetLessonDto>>> GetLessons();
    Task<Response<GetLessonDto>> GetLessonById(int id);
}