using System.Net;
using AutoMapper;
using Domain.Dto.Lesson;
using Domain.Filter;
using Infrastructure.Repositories.Lesson;
using Infrastructure.Response;

namespace Infrastructure.Services.Lesson;

public class LessonService(ILessonRepository lessonRepository, IMapper mapper) : ILessonService
{
    public async Task<Response<string>> CreateLessonAsync(CreateLessonDto dto)
    {
        var mapped = mapper.Map<Domain.Entities.Lesson>(dto);
        var result = await lessonRepository.CreateLessonAsync(mapped);
        return result > 0
            ? new Response<string>(HttpStatusCode.Created, "Lesson created successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "Fail to create lesson");
    }

    public async Task<Response<string>> UpdateLessonAsync(UpdateLessonDto dto)
    {
        var lesson = await lessonRepository.GetLessonByIdAsync(dto.Id);
        if (lesson is null) return new Response<string>(HttpStatusCode.NotFound, "Lesson not found");
        
        mapper.Map(dto, lesson);
        var result = await lessonRepository.UpdateLessonAsync(lesson);
        
        return result > 0
            ? new Response<string>(HttpStatusCode.OK, "Lesson updated successfully")
            : new Response<string>(HttpStatusCode.NotFound, "Lesson update failed");
    }

    public async Task<Response<string>> DeleteLessonAsync(Guid id)
    {
        var result = await lessonRepository.DeleteLessonAsync(id);
        if (result is null) return new Response<string>(HttpStatusCode.NotFound, "Not found lesson");
        return result > 0
            ? new Response<string>(HttpStatusCode.OK, "Lesson deleted successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "Something went wrong");
    }

    public async Task<Response<List<GetLessonDto>>> GetLessonsAsync(LessonFilter filter)
    {
        var lessons = await lessonRepository.GetLessonsAsync(filter);
        if (lessons is null) return new Response<List<GetLessonDto>>(HttpStatusCode.NotFound, "Lessons not found");
        
        var mapped = mapper.Map<List<GetLessonDto>>(lessons);
        return new PaginationResponse<List<GetLessonDto>>(mapped, mapped.Count, filter.PageNumber, filter.PageSize);
    }

    public async Task<Response<GetLessonDto>> GetLessonByIdAsync(Guid id)
    {
        var lesson = await lessonRepository.GetLessonByIdAsync(id);
        if (lesson is null) return new Response<GetLessonDto>(HttpStatusCode.NotFound, "Lesson not found");
        var dto = mapper.Map<GetLessonDto>(lesson);
        return new Response<GetLessonDto>(dto);
    }
}
