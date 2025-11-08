using Domain.Dto.Lesson;
using Domain.Filter;
using Infrastructure.Response;

namespace Infrastructure.Repositories.Lesson;

public interface ILessonRepository
{
    Task<int> CreateLessonAsync(Domain.Entities.Lesson dto);
    Task<int> UpdateLessonAsync(Domain.Entities.Lesson dto);
    Task<int?> DeleteLessonAsync(Guid lessonId);
    Task<List<Domain.Entities.Lesson>?> GetLessonsAsync(LessonFilter filter);
    Task<Domain.Entities.Lesson?> GetLessonByIdAsync(Guid lessonId);
}