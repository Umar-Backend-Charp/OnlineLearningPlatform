using System.Net;
using Domain.Dto;
using Domain.Dto.Lesson;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Response;
using Infrastructure.Services.Lesson;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class LessonService(DataContext context) : ILessonService
{
    public async Task<Response<string>> CreateLesson(CreateLessonDto dto)
    {
        var lesson = new Domain.Entities.Lesson
        {
            CourseId = dto.CourseId,
            Title = dto.Title,
            Content = dto.Content,
            Order = dto.Order,
        };
        await context.Lessons.AddAsync(lesson);
        var effect = await context.SaveChangesAsync();
        return effect > 0
            ? new Response<string>(HttpStatusCode.OK, "Lesson created successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "Lesson creation failed");
    }

    public async Task<Response<string>> UpdateLesson(UpdateLessonDto dto)
    {
        var oldLesson = await context.Lessons.FirstOrDefaultAsync(x => x.Id == dto.Id);
        if (oldLesson == null) return new Response<string>(HttpStatusCode.NotFound, "Lesson not found");
        oldLesson.Title = dto.Title;
        oldLesson.Content = dto.Content;
        oldLesson.Order = dto.Order;
        oldLesson.IsDeleted = dto.IsDeleted;
        oldLesson.CourseId = dto.CourseId;
        oldLesson.UpdateAt = DateTime.UtcNow;
        var effect = await context.SaveChangesAsync();
        return effect > 0
            ? new Response<string>(HttpStatusCode.OK, "Lesson updated successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "Lesson updating failed");
    }

    public async Task<Response<string>> DeleteLesson(int id)
    {
        var lesson = await context.Lessons.FirstOrDefaultAsync(x => x.Id == id);
        if (lesson == null) return new Response<string>(HttpStatusCode.NotFound, "Lesson not found");
        lesson.IsDeleted = true;
        lesson.UpdateAt = DateTime.UtcNow;
        var effect = await context.SaveChangesAsync();
        return effect > 0
            ? new Response<string>(HttpStatusCode.OK, "Lesson deleted successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "Lesson deletion failed");
    }

    public async Task<Response<List<GetLessonDto>>> GetLessons()
    {
        var lessons = await context.Lessons.Where(x => !x.IsDeleted).ToListAsync();
        if (lessons == null) return new Response<List<GetLessonDto>>(HttpStatusCode.BadRequest, "Lessons not found");
        var dtos = lessons.Select(x => new GetLessonDto()
        {
            Id = x.Id,
            Title = x.Title,
            CourseId = x.CourseId,
            IsDeleted = x.IsDeleted,
            CreateAt = x.CreateAt,
            UpdateAt = x.UpdateAt,
            Order = x.Order,
            Content = x.Content
        }).ToList();

        return new Response<List<GetLessonDto>>(dtos);
    }

    public async Task<Response<GetLessonDto>> GetLessonById(int id)
    {
        var lesson = await context.Lessons.FirstOrDefaultAsync(x => x.Id == id);
        if (lesson == null) return new Response<GetLessonDto>(HttpStatusCode.NotFound, "Lesson not found");

        var dto = new GetLessonDto()
        {
            Id = lesson.Id,
            Title = lesson.Title,
            CourseId = lesson.CourseId,
            Content = lesson.Content,
            Order = lesson.Order,
            IsDeleted = lesson.IsDeleted,
            CreateAt = lesson.CreateAt,
            UpdateAt = lesson.UpdateAt,
        };

        return new Response<GetLessonDto>(dto);
    }
}
