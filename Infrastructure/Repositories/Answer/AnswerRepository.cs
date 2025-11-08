using Domain.Dto.UserAnswer;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Answer;

public class AnswerRepository(DataContext context) : IAnswerRepository
{
    public async Task<int> CreateAnswerAsync(Domain.Entities.Answer answer)
    {
        await context.Answers.AddAsync(answer);
        return await context.SaveChangesAsync();
    }

    public async Task<int> UpdateAnswerAsync(Domain.Entities.Answer answer)
    {
        context.Update(answer);
        answer.UpdatedAt = DateTime.UtcNow;
        return await context.SaveChangesAsync();
    }

    public async Task<int?> DeleteAnswerAsync(Guid id)
    {
        var answer = await context.Answers.FindAsync(id);
        if (answer == null) return null;
        context.Answers.Remove(answer);
        return await context.SaveChangesAsync();
    }

    public async Task<List<Domain.Entities.Answer>> GetAllAnswersAsync(Guid questionId)
    {
        return await context.Answers.Where(a => a.QuestionId == questionId).ToListAsync();
    }

    public async Task<Domain.Entities.Answer?> GetAnswerByIdAsync(Guid answerId)
    {
        return await context.Answers.FindAsync(answerId);
    }
}