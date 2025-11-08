using Domain.Dto;
using Domain.Dto.Auth;
using Domain.Dto.Course;
using Domain.Dto.Exam;
using Domain.Dto.Lesson;
using Domain.Dto.Question;
using Domain.Dto.StudentExamResult;
using Domain.Dto.User;
using Domain.Dto.UserAnswer;
using Domain.Entities;

namespace Infrastructure.Profile;

public class ApplicationProfile : AutoMapper.Profile
{
    public ApplicationProfile()
    {
        CreateMap<Register, User>();
        CreateMap<User, GetUserDto>();
        CreateMap<UpdateUserDto, User>();

        CreateMap<CreateCourseDto, Course>();
        CreateMap<Course, GetCourseDto>();
        CreateMap<UpdateCourseDto, Course>();
        CreateMap<Course, GetCourseForSummaryDto>();
        
        CreateMap<CreateLessonDto, Lesson>();
        CreateMap<UpdateLessonDto, Lesson>();
        CreateMap<Lesson, GetLessonDto>();

        CreateMap<CreateExamDto, Exam>();
        CreateMap<UpdateExamDto, Exam>();
        CreateMap<Exam, GetExamDto>();
        
        CreateMap<CreateQuestionDto, Question>();
        CreateMap<UpdateQuestionDto, Question>();
        CreateMap<Question, GetQuestionDto>();

        CreateMap<CreateAnswerDto, Answer>();
        CreateMap<UpdateAnswerDto, Answer>();
        CreateMap<Answer, GetAnswerDto>();

        CreateMap<CreateStudentExamResult, StudentExamResult>();
        CreateMap<StudentExamResult, GetStudentExamResult>();
    }
}