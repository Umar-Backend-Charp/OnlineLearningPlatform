using System.Net;
using AutoMapper;
using Domain.Dto.UserAnswer;
using Infrastructure.Repositories.Answer;
using Infrastructure.Repositories.Question;
using Infrastructure.Response;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.Answer;

public class AnswerService(IAnswerRepository answerRepository, 
    IMapper mapper, 
    IQuestionRepository questionRepository, 
    UserManager<Domain.Entities.User> userManager) : IAnswerService
{
    public async Task<Response<string>> CreateAnswerAsync(CreateAnswerDto answerDto)
    {
        var existingQuestion = await questionRepository.GetQuestionByIdAsync(answerDto.QuestionId);
        if (existingQuestion == null) return new Response<string>(HttpStatusCode.NotFound, "Not found the question");

        var existingUser = await userManager.FindByIdAsync(answerDto.UserId);
        if (existingUser == null) return new Response<string>(HttpStatusCode.NotFound, "Not found the user");
        
        var answer = mapper.Map<Domain.Entities.Answer>(answerDto);
        answer.IsCorrect = answer.Text == existingQuestion.CorrectAnswer;
        
        var result = await answerRepository.CreateAnswerAsync(answer);

        return result > 0
            ? new Response<string>(HttpStatusCode.Created, "Created answer successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "Failed to create answer");
    }

    public async Task<Response<string>> UpdateAnswerAsync(UpdateAnswerDto dto)
    {
        var answer = await answerRepository.GetAnswerByIdAsync(dto.Id);
        if (answer == null) return new Response<string>(HttpStatusCode.NotFound, "Not found the answer");
        
        mapper.Map(dto, answer);
        var result = await answerRepository.UpdateAnswerAsync(answer);
        
        return result > 0
            ? new Response<string>(HttpStatusCode.OK, "Updated answer successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "Failed to update answer");
    }

    public async Task<Response<string>> DeleteAnswerAsync(Guid id)
    {
        var result = await answerRepository.DeleteAnswerAsync(id);
        return result is null
            ? new Response<string>(HttpStatusCode.NotFound, "Not found the answer") 
            : new Response<string>(HttpStatusCode.OK, "Deleted answer successfully");
    }

    public async Task<Response<List<GetAnswerDto>>> GetAnswersAsync(Guid questionId)
    {
        var answerList = await answerRepository.GetAllAnswersAsync(questionId);
        if (answerList.Count < 1) return new Response<List<GetAnswerDto>>(HttpStatusCode.NotFound, "Not found the answer");

        var dtoList = mapper.Map<List<GetAnswerDto>>(answerList);
        return new Response<List<GetAnswerDto>>(dtoList);
    }

    public async Task<Response<GetAnswerDto>> GetAnswerByIdAsync(Guid answerId)
    {
        var answer = await answerRepository.GetAnswerByIdAsync(answerId);
        if (answer == null) return new Response<GetAnswerDto>(HttpStatusCode.NotFound, "Not found the answer");

        var dto = mapper.Map<GetAnswerDto>(answer);
        return new Response<GetAnswerDto>(dto);
    }
}