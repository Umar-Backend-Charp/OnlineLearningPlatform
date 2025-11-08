using Domain.Dto.Question;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Question;

public class QuestionRepository(DataContext context) : IQuestionRepository
{
    public async Task<int> CreateQuestionAsync(Domain.Entities.Question question)
    {
        await context.Questions.AddAsync(question);
        return await context.SaveChangesAsync();
    }

    public async Task<int> UpdateQuestionAsync(Domain.Entities.Question question)
    {
        context.Questions.Update(question);
        question.UpdatedAt = DateTime.UtcNow;
        return await context.SaveChangesAsync();
    }

    public async Task<int?> DeleteQuestionAsync(Guid questionId)
    {
        var question = await context.Questions.FindAsync(questionId);
        if (question is null) return null;
        question.IsDeleted = true;
        return await context.SaveChangesAsync();
    }

    public async Task<List<Domain.Entities.Question>> GetQuestionsAsync(Guid examId)
    {
        return await context.Questions.Where(q => !q.IsDeleted && q.ExamId == examId).ToListAsync();
    }

    public async Task<Domain.Entities.Question?> GetQuestionByIdAsync(Guid questionId)
    {
        return await context.Questions.FindAsync(questionId);
    }
}