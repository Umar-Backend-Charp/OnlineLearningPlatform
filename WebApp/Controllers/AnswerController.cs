using Domain.Dto.UserAnswer;
using Infrastructure.Response;
using Infrastructure.Services.Answer;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnswerController(IAnswerService answerService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<string>> CreateAnswerAsync(CreateAnswerDto dto)
        => await answerService.CreateAnswerAsync(dto);

    [HttpGet]
    public async Task<Response<List<GetAnswerDto>>> GetAllAnswersAsync(Guid questionId)
        => await answerService.GetAnswersAsync(questionId);

    [HttpGet("{id}")]
    public async Task<Response<GetAnswerDto>> GetAnswerAsync(Guid id)
        => await answerService.GetAnswerByIdAsync(id);
    
    [HttpPut]
    public async Task<Response<string>> UpdateAnswerAsync(UpdateAnswerDto dto)
        => await answerService.UpdateAnswerAsync(dto);
    
    [HttpDelete]
    public async Task<Response<string>> DeleteAnswerAsync(Guid id)
        => await answerService.DeleteAnswerAsync(id);

}