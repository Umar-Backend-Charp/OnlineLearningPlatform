using System.Net;
using AutoMapper;
using Domain.Dto.Exam;
using Domain.Filter;
using Infrastructure.Data;
using Infrastructure.Repositories.Exam;
using Infrastructure.Response;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Exam;

public class ExamService(IExamRepository examRepository, IMapper mapper) : IExamService
{
    public async Task<Response<string>> CreateExamAsync(CreateExamDto dto)
    {
        var mapped = mapper.Map<Domain.Entities.Exam>(dto);
            
        var result = await examRepository.CreateExamAsync(mapped);

        return result > 0
            ? new Response<string>(HttpStatusCode.OK, "Exam created successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "Exam creation failed");
    }

    public async Task<Response<string>> UpdateExamAsync(UpdateExamDto dto)
    {
        var oldExam = await examRepository.GetExamByIdAsync(dto.Id);
        if (oldExam is null)
            return new Response<string>(HttpStatusCode.NotFound, "Exam not found");
        
        mapper.Map(dto, oldExam);
        var result = await examRepository.UpdateExamAsync(oldExam);
        
        return result > 0
            ? new Response<string>(HttpStatusCode.OK, "Exam updated successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "Exam updating failed");
    }

    public async Task<Response<string>> DeleteExamAsync(Guid id)
    {
        var result = await examRepository.DeleteExamAsync(id);
        if (result is null)
            return new Response<string>(HttpStatusCode.NotFound, "Exam not found");
        
        return result > 0
            ? new Response<string>(HttpStatusCode.OK, "Exam deleted successfully")
            : new Response<string>(HttpStatusCode.BadRequest, "Exam deletion failed");
    }

    public async Task<PaginationResponse<List<GetExamDto>>> GetExamsAsync(ExamFilter filter)
    {
        var exams = await examRepository.GetExamsAsync(filter);
        if (exams is null)
            return new PaginationResponse<List<GetExamDto>>(HttpStatusCode.NotFound, "Exams not found");

        var mappedList = mapper.Map<List<GetExamDto>>(exams);
        
        return new PaginationResponse<List<GetExamDto>>(mappedList, exams.Count, filter.PageNumber, filter.PageSize);
    }

    public async Task<Response<GetExamDto>> GetExamByIdAsync(Guid id)
    {
        var exam = await examRepository.GetExamByIdAsync(id);
        if (exam is null) 
            return new Response<GetExamDto>(HttpStatusCode.NotFound, "Exam not found");
        
        var dto = mapper.Map<GetExamDto>(exam);

        return new Response<GetExamDto>(dto);
    }
}
