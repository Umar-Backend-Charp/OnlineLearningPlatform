using AutoMapper;
using Domain.Dto.Lesson;
using Domain.Filter;
using Infrastructure.Data;
using Infrastructure.Response;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Lesson;

public class LessonRepository(DataContext context) : ILessonRepository
{
    public async Task<int> CreateLessonAsync(Domain.Entities.Lesson lesson)
    {
        var maxOrder = await context.Lessons
            .Where(l => l.CourseId == lesson.CourseId && !l.IsDeleted)
            .MaxAsync(l => (int?)l.Order) ?? 0;
        
        lesson.Order = maxOrder + 1;
        await context.Lessons.AddAsync(lesson);
        return await context.SaveChangesAsync();
    }

    public async Task<int> UpdateLessonAsync(Domain.Entities.Lesson lesson)
    {
        context.Lessons.Update(lesson);
        lesson.UpdateAt = DateTime.UtcNow;
        return await context.SaveChangesAsync();
    }

    public async Task<int?> DeleteLessonAsync(Guid lessonId)
    {
        var lesson = await context.Lessons.FirstOrDefaultAsync(l => l.Id == lessonId);;
        if (lesson is null) return null;
        lesson.IsDeleted = true;
        return await context.SaveChangesAsync();
    }

    public async Task<List<Domain.Entities.Lesson>?> GetLessonsAsync(LessonFilter filter)
    {
        var query = context.Lessons.AsQueryable();

        query = query.Where(l => !l.IsDeleted);
        
        if (filter.CourseId.HasValue)
            query = query.Where(l => l.CourseId == filter.CourseId);
        
        if (!string.IsNullOrEmpty(filter.Title))
            query = query.Where(l => l.Title.ToLower().Contains(filter.Title.ToLower()));
        
        var totalCount = query.Count();
        var skip = (filter.PageNumber - 1) * filter.PageSize;
        
        var result = await query.Skip(skip).Take(filter.PageSize).ToListAsync();
        return result;
    }

    public async Task<Domain.Entities.Lesson?> GetLessonByIdAsync(Guid lessonId)
    {
        return await context.Lessons.FirstOrDefaultAsync(l => l.Id == lessonId);
    }
}