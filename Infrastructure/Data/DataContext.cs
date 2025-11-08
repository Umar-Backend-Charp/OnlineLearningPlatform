using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<StudentExamResult> StudentExamResults { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<StudentCourse>()
            .HasKey(sc => new { sc.UserId, sc.CourseId });
        
        builder.Entity<StudentCourse>()
            .HasOne(sc => sc.User)
            .WithMany(u => u.StudentCourses)
            .HasForeignKey(sc => sc.UserId);
        
        builder.Entity<StudentCourse>()
            .HasOne(sc => sc.Course)
            .WithMany(c => c.StudentCourses)
            .HasForeignKey(sc => sc.CourseId);
        
        base.OnModelCreating(builder);
    }
}