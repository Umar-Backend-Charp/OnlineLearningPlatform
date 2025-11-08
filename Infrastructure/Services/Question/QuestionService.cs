using System.Net;
using AutoMapper;
using Domain.Dto.Question;
using Infrastructure.Repositories.Question;
using Infrastructure.Response;

namespace Infrastructure.Services.Question;

public class QuestionService(IQuestionRepository questionRepository, IMapper mapper) : IQuestionService
{
    public async Task<Response<string>> CreateQuestionAsync(CreateQuestionDto question)
    {
        var mapped = mapper.Map<Domain.Entities.Question>(question);
        var result = await questionRepository.CreateQuestionAsync(mapped);

        return result > 0
            ? new Response<string>(HttpStatusCode.Created, "Question created successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "Failed to create question");
    }

    public async Task<Response<string>> UpdateQuestionAsync(UpdateQuestionDto dto)
    {
        var question = await questionRepository.GetQuestionByIdAsync(dto.Id);
        if (question is null) return new Response<string>(HttpStatusCode.NotFound, "Question not found");
        
        mapper.Map(dto, question);
        var result = await questionRepository.UpdateQuestionAsync(question);

        return result > 0
            ? new Response<string>(HttpStatusCode.OK, "Question updated successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "Failed to update question");
    }

    public async Task<Response<string>> DeleteQuestionAsync(Guid questionId)
    {
        var result = await questionRepository.DeleteQuestionAsync(questionId);
        if (result is null) return new Response<string>(HttpStatusCode.NotFound, "Question not found");

        return result > 0
            ? new Response<string>(HttpStatusCode.OK, "Question deleted successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "Failed to delete question");
    }

    public async Task<Response<List<GetQuestionDto>>> GetQuestionsAsync(Guid examId)
    {
        var questions = await questionRepository.GetQuestionsAsync(examId);
        if (questions.Count <= 0) return new Response<List<GetQuestionDto>>(HttpStatusCode.NotFound, "Questions not found");

        var dtoList = mapper.Map<List<GetQuestionDto>>(questions);
        return new Response<List<GetQuestionDto>>(dtoList);
    }

    public async Task<Response<GetQuestionDto>> GetQuestionByIdAsync(Guid questionId)
    {
        var question = await questionRepository.GetQuestionByIdAsync(questionId);
        if (question is null) return new Response<GetQuestionDto>(HttpStatusCode.NotFound, "Not found the question");

        var dto = mapper.Map<GetQuestionDto>(question);
        return new Response<GetQuestionDto>(dto);
    }
}