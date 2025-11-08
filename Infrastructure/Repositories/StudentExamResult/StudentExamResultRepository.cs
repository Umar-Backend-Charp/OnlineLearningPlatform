using Domain.Dto.StudentExamResult;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.StudentExamResult;

public class StudentExamResultRepository(DataContext context) : IStudentExamResultRepository
{
    public async Task<int> CreateStudentExamResultAsync(Domain.Entities.StudentExamResult studentExamResult)
    {
        studentExamResult.Passed = studentExamResult.Score > 80;
        await context.StudentExamResults.AddAsync(studentExamResult);
        return await context.SaveChangesAsync();
    }

    public async Task<Domain.Entities.StudentExamResult?> GetStudentExamResultByIdAsync(Guid id)
    {
        return await context.StudentExamResults.FindAsync(id);
    }

    public async Task<List<Domain.Entities.StudentExamResult>> GetAllStudentExamResultAsync()
    {
        return await context.StudentExamResults.ToListAsync();
    }
}