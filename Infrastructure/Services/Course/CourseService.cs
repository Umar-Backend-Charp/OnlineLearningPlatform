using System.Net;
using Domain.Dto.Course;
using Infrastructure.Data;
using Infrastructure.Response;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Course;

public class CourseService(DataContext context) : ICourseService
{
    public async Task<Response<string>> CreateCourse(CreateCourseDto dto)
    {
        var course = new Domain.Entities.Course
        {
            Title = dto.Title,
            Description = dto.Description,
            Level = dto.Level,
            Price = dto.Price,
        };
        await context.Courses.AddAsync(course);
        var effect = await context.SaveChangesAsync();
        return effect > 0
            ? new Response<string>(HttpStatusCode.OK, "Course has been created")
            : new Response<string>(HttpStatusCode.BadRequest, "Failed to create course");
    }

    public async Task<Response<string>> UpdateCourse(UpdateCourseDto dto)
    {
        var oldCourse = await context.Courses.FirstOrDefaultAsync(x => x.Id == dto.Id);
        if (oldCourse == null) return new Response<string>(HttpStatusCode.NotFound, "Course not found");
        oldCourse.Title = dto.Title;
        oldCourse.Description = dto.Description;
        oldCourse.Level = dto.Level;
        oldCourse.Price = dto.Price;
        var effect = await context.SaveChangesAsync();
        return effect > 0
            ?  new Response<string>(HttpStatusCode.OK, "Course has been updated")
            :  new Response<string>(HttpStatusCode.BadRequest, "Failed update course");
    }

    public async Task<Response<string>> DeleteCourse(int id)
    {
        var  course = await context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        if (course == null) return new Response<string>(HttpStatusCode.NotFound, "Course not found");
        course.IsDeleted = true;
        var effect = await context.SaveChangesAsync();
        return effect > 0
            ? new Response<string>(HttpStatusCode.OK, "Course has been deleted")
            :  new Response<string>(HttpStatusCode.BadRequest, "Failed delete course");
    }

    public async Task<Response<List<GetCourseDto>>> GetCourses()
    {
        var courses = await context.Courses.ToListAsync();
        if (courses == null) return new Response<List<GetCourseDto>>(HttpStatusCode.NotFound, "Courses not found");
        var dtos = courses.Select(c => new GetCourseDto
        {
            Title = c.Title,
            Description = c.Description,
            Price = c.Price,
            Level = c.Level,
            IsDeleted = c.IsDeleted,
            CreateAt = c.CreateAt,
            UpdateAt = c.UpdateAt,
            Id = c.Id
        }).ToList();
        return new Response<List<GetCourseDto>>(dtos);
    }

    public async Task<Response<GetCourseDto>> GetCourseById(int id)
    {
        var course = await context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        if (course == null) return new Response<GetCourseDto>(HttpStatusCode.NotFound, "Course not found");
        var dto = new GetCourseDto
        {
            Title = course.Title,
            Description = course.Description,
            Level = course.Level,
            Price = course.Price,
            CreateAt = course.CreateAt,
            UpdateAt = course.UpdateAt,
            IsDeleted = course.IsDeleted,
        };
        return new Response<GetCourseDto>(dto);
    }
}