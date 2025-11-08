using System.Net;
using AutoMapper;
using Domain.Dto.Course;
using Domain.Dto.User;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Response;
using Infrastructure.Services.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Course;

public class CourseService(DataContext context, UserManager<Domain.Entities.User> userManager, IMapper mapper) : ICourseService
{
    public async Task<Response<string>> CreateCourse(CreateCourseDto dto)
    {
        var mapped = mapper.Map<Domain.Entities.Course>(dto);
        await context.Courses.AddAsync(mapped);
        var effect = await context.SaveChangesAsync();
        return effect > 0
            ? new Response<string>(HttpStatusCode.OK, "Course has been created")
            : new Response<string>(HttpStatusCode.BadRequest, "Failed to create course");
    }

    public async Task<Response<string>> UpdateCourse(UpdateCourseDto dto)
    {
        var oldCourse = await context.Courses.FirstOrDefaultAsync(x => x.Id == dto.Id);
        if (oldCourse == null) return new Response<string>(HttpStatusCode.NotFound, "Course not found");
        mapper.Map(dto, oldCourse);
        var effect = await context.SaveChangesAsync();
        return effect > 0
            ?  new Response<string>(HttpStatusCode.OK, "Course has been updated")
            :  new Response<string>(HttpStatusCode.BadRequest, "Failed update course");
    }

    public async Task<Response<string>> DeleteCourse(string id)
    {
        var  course = await context.Courses.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        if (course == null) return new Response<string>(HttpStatusCode.NotFound, "Course not found");
        course.IsDeleted = true;
        var effect = await context.SaveChangesAsync();
        return effect > 0
            ? new Response<string>(HttpStatusCode.OK, "Course has been deleted")
            :  new Response<string>(HttpStatusCode.BadRequest, "Failed delete course");
    }

    public async Task<Response<List<GetCourseDto>>> GetCourses()
    {
        var courses = await context.Courses
            .Where(c => c.IsDeleted == false)
            .Include(c => c.StudentCourses)
            .ThenInclude(sc => sc.User).ToListAsync();
        
        if (!(courses.Count > 0)) return new Response<List<GetCourseDto>>(HttpStatusCode.NotFound, "Courses not found");
        
        var mappedCourses = mapper.Map<List<GetCourseDto>>(courses);

        foreach (var courseDto in mappedCourses)
        {
            var course = courses.First(c => c.Id == courseDto.Id);
            var studentsDto = new List<GetUserDto>();

            foreach (var studentCourse in course.StudentCourses)
            {
                var roles = await userManager.GetRolesAsync(studentCourse.User!);
                var userDto = mapper.Map<GetUserDto>(studentCourse.User!);
                userDto.Roles = roles.ToList();
                studentsDto.Add(userDto);
            }
            
            courseDto.Students = studentsDto;
        }
        
        return new Response<List<GetCourseDto>>(mappedCourses);
    }

    public async Task<Response<GetCourseDto>> GetCourseById(string id)
    {
        var course = await context.Courses.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        if (course == null) return new Response<GetCourseDto>(HttpStatusCode.NotFound, "Course not found");
        
        var mappedCourse = mapper.Map<GetCourseDto>(course);

        var students = await context.StudentCourses
            .Include(sc => sc.User)
            .Where(sc => sc.CourseId.ToString() == id)
            .Select(sc => sc.User)
            .ToListAsync();

        var studentsDto = new List<GetUserDto>();

        foreach (var user in students)
        {
            var roles = await userManager.GetRolesAsync(user!);
            var userDto = mapper.Map<GetUserDto>(user);
            userDto.Roles = roles.ToList();
            studentsDto.Add(userDto);
        }
        
        mappedCourse.Students = studentsDto;
        return new Response<GetCourseDto>(mappedCourse);
    }

    public async Task<Response<string>> EnrollStudent(string courseId, string studentId)
    {
        var existingStudent = await context.Users.FirstOrDefaultAsync(x => x.Id == studentId);
        if (existingStudent is null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Not found student");
        } 
        
        var existingCourse = await context.Courses.FirstOrDefaultAsync(x => x.Id.ToString() == courseId);
        if (existingCourse is null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Not found course");
        }

        var alreadyEnrolled = await context.StudentCourses.AnyAsync(sc =>
            sc.UserId == studentId && sc.CourseId.ToString() == courseId);
        
        if (alreadyEnrolled)
            return new Response<string>(HttpStatusCode.BadRequest, "Student already enrolled");

        await context.StudentCourses.AddAsync(new StudentCourse()
        {
            UserId = studentId,
            CourseId = Guid.Parse(courseId)
        });

        var result = await context.SaveChangesAsync();
        
        return result > 0
            ? new Response<string>(HttpStatusCode.OK, "Student has been enrolled")
            : new Response<string>(HttpStatusCode.BadRequest, "Failed enroll student");
    }
}