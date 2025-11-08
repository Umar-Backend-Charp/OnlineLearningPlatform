using Domain.Dto.UserAnswer;
using Infrastructure.Response;
using Infrastructure.Services.Answer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnswerController(IAnswerService answerService) : ControllerBase
{
    [Authorize]
    [HttpPost]
    public async Task<Response<string>> CreateAnswerAsync(CreateAnswerDto dto)
        => await answerService.CreateAnswerAsync(dto);

    [Authorize(Roles = "Teacher, Admin")]
    [HttpGet]
    public async Task<Response<List<GetAnswerDto>>> GetAllAnswersAsync(Guid questionId)
        => await answerService.GetAnswersAsync(questionId);

    [Authorize(Roles = "Teacher, Admin")]
    [HttpGet("{id}")]
    public async Task<Response<GetAnswerDto>> GetAnswerAsync(Guid id)
        => await answerService.GetAnswerByIdAsync(id);
    
    [Authorize(Roles = "Teacher, Admin")]
    [HttpPut]
    public async Task<Response<string>> UpdateAnswerAsync(UpdateAnswerDto dto)
        => await answerService.UpdateAnswerAsync(dto);
    
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<Response<string>> DeleteAnswerAsync(Guid id)
        => await answerService.DeleteAnswerAsync(id);

}