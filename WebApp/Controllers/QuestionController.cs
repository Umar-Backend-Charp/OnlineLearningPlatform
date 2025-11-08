using Domain.Dto.Question;
using Infrastructure.Response;
using Infrastructure.Services.Question;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/questions")]
public class QuestionController(IQuestionService questionService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<string>> CreateQuestion([FromBody] CreateQuestionDto dto)
    {
        return await questionService.CreateQuestionAsync(dto);
    }
    
    [HttpGet]
    public async Task<Response<List<GetQuestionDto>>> GetAllQuestions(Guid examId)
    {
        return await questionService.GetQuestionsAsync(examId);
    }
    
    [HttpGet("{questionId}")]
    public async Task<Response<GetQuestionDto>> GetQuestionById([FromRoute] Guid questionId)
    {
        return await questionService.GetQuestionByIdAsync(questionId);
    } 
    
    [HttpPut]
    public async Task<Response<string>> UpdateQuestion([FromBody] UpdateQuestionDto dto)
    {
        return await questionService.UpdateQuestionAsync(dto);
    }
    
    [HttpDelete]
    public async Task<Response<string>> DeleteQuestion(Guid questionId)
    {
        return await questionService.DeleteQuestionAsync(questionId);
    }
}