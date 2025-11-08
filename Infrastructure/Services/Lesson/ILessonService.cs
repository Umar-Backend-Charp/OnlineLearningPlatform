using Domain.Dto.Lesson;
using Domain.Filter;
using Infrastructure.Response;

namespace Infrastructure.Services.Lesson;

public interface ILessonService
{
    Task<Response<string>> CreateLessonAsync(CreateLessonDto dto);
    Task<Response<string>> UpdateLessonAsync(UpdateLessonDto dto);
    Task<Response<string>> DeleteLessonAsync(Guid lessonId);
    Task<Response<List<GetLessonDto>>> GetLessonsAsync(LessonFilter filter);
    Task<Response<GetLessonDto>> GetLessonByIdAsync(Guid lessonId);
}