using System.Net;
using Domain.Dto.Exam;
using Infrastructure.Data;
using Infrastructure.Response;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Exam;

public class ExamService(DataContext context) : IExamService
{
    public async Task<Response<string>> CreateExam(CreateExamDto dto)
    {
        var exam = new Domain.Entities.Exam
        {
            CourseId = dto.CourseId,
            Title = dto.Title,
            MaxScore = dto.MaxScore,
            CreateAt = DateTime.Now,
        };

        await context.Exams.AddAsync(exam);
        var effect = await context.SaveChangesAsync();

        return effect > 0
            ? new Response<string>(HttpStatusCode.OK, "Exam created successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "Exam creation failed");
    }

    public async Task<Response<string>> UpdateExam(UpdateExamDto dto)
    {
        var oldExam = await context.Exams.FirstOrDefaultAsync(x => x.Id == dto.Id);
        if (oldExam == null) return new Response<string>(HttpStatusCode.NotFound, "Exam not found");

        oldExam.Title = dto.Title;
        oldExam.MaxScore = dto.MaxScore;
        oldExam.CourseId = dto.CourseId;
        oldExam.UpdateAt = DateTime.UtcNow;

        var effect = await context.SaveChangesAsync();
        return effect > 0
            ? new Response<string>(HttpStatusCode.OK, "Exam updated successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "Exam updating failed");
    }

    public async Task<Response<string>> DeleteExam(int id)
    {
        var exam = await context.Exams.FirstOrDefaultAsync(x => x.Id == id);
        if (exam == null) return new Response<string>(HttpStatusCode.NotFound, "Exam not found");

        exam.IsDeleted = true;
        exam.UpdateAt = DateTime.UtcNow;
        var effect = await context.SaveChangesAsync();

        return effect > 0
            ? new Response<string>(HttpStatusCode.OK, "Exam deleted successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "Exam deletion failed");
    }

    public async Task<Response<List<GetExamDto>>> GetExams()
    {
        var exams = await context.Exams.Where(x => !x.IsDeleted).ToListAsync();
        var dtos = exams.Select(x => new GetExamDto
        {
            Id = x.Id,
            Title = x.Title,
            CourseId = x.CourseId,
            MaxScore = x.MaxScore,
            IsDeleted = x.IsDeleted,
            CreateAt = x.CreateAt,
            UpdateAt = x.UpdateAt
        }).ToList();

        return new Response<List<GetExamDto>>(dtos);
    }

    public async Task<Response<GetExamDto>> GetExamById(int id)
    {
        var exam = await context.Exams.FirstOrDefaultAsync(x => x.Id == id);
        if (exam == null) return new Response<GetExamDto>(HttpStatusCode.NotFound, "Exam not found");

        var dto = new GetExamDto
        {
            Id = exam.Id,
            Title = exam.Title,
            CourseId = exam.CourseId,
            MaxScore = exam.MaxScore,
            IsDeleted = exam.IsDeleted,
            CreateAt = exam.CreateAt,
            UpdateAt = exam.UpdateAt
        };

        return new Response<GetExamDto>(dto);
    }
}
