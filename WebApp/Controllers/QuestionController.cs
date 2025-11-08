using Domain.Dto.Question;
using Infrastructure.Response;
using Infrastructure.Services.Question;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/questions")]
public class QuestionController(IQuestionService questionService) : ControllerBase
{
    [Authorize(Roles = "Teacher, Admin")]
    [HttpPost]
    public async Task<Response<string>> CreateQuestion([FromBody] CreateQuestionDto dto)
    {
        return await questionService.CreateQuestionAsync(dto);
    }
    
    [Authorize(Roles = "Teacher, Admin")]
    [HttpGet]
    public async Task<Response<List<GetQuestionDto>>> GetAllQuestions(Guid examId)
    {
        return await questionService.GetQuestionsAsync(examId);
    }
    
    [Authorize]
    [HttpGet("{questionId}")]
    public async Task<Response<GetQuestionDto>> GetQuestionById([FromRoute] Guid questionId)
    {
        return await questionService.GetQuestionByIdAsync(questionId);
    } 
    
    [Authorize(Roles = "Teacher, Admin")]
    [HttpPut]
    public async Task<Response<string>> UpdateQuestion([FromBody] UpdateQuestionDto dto)
    {
        return await questionService.UpdateQuestionAsync(dto);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<Response<string>> DeleteQuestion(Guid questionId)
    {
        return await questionService.DeleteQuestionAsync(questionId);
    }
}