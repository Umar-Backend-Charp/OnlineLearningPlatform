using Domain.Filter;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Exam;

public class ExamRepository(DataContext context) : IExamRepository
{
    public async Task<int> CreateExamAsync(Domain.Entities.Exam model)
    {
        await context.Exams.AddAsync(model);
        return await context.SaveChangesAsync();
    }

    public async Task<int> UpdateExamAsync(Domain.Entities.Exam model)
    {
        context.Exams.Update(model);
        model.UpdateAt = DateTime.UtcNow;
        return await context.SaveChangesAsync();
    }

    public async Task<int?> DeleteExamAsync(Guid examId)
    {
        var exam = await context.Exams.FindAsync(examId);
        if (exam is null) return null;
        exam.IsDeleted =  true;
        var result = await context.SaveChangesAsync();
        return result;
    }

    public async Task<List<Domain.Entities.Exam>?> GetExamsAsync(ExamFilter filter)
    {
        var query = context.Exams
            .Include(e => e.Questions.Where(q => !q.IsDeleted))
            .AsQueryable();

        query = query.Where(e => !e.IsDeleted);
        
        if (filter.CourseId.HasValue)
            query = query.Where(e => e.CourseId == filter.CourseId);
        
        var skip = (filter.PageNumber - 1) * filter.PageSize;
        
        var result = await query.Skip(skip).Take(filter.PageSize).ToListAsync();
        return result;
    }

    public async Task<Domain.Entities.Exam?> GetExamByIdAsync(Guid examId)
    {
        return await context.Exams.Include(e => e.Questions).FirstOrDefaultAsync(e => e.Id == examId);
    }
}