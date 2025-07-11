using CoursePro.Application.DTOs.Courses;

namespace CoursePro.Application.Services.Contracts
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllAsync();
        Task<CourseDto?> GetByIdAsync(Guid id);
        Task<CourseDto> CreateAsync(CreateCourseDto dto);
        Task<bool> UpdateAsync(UpdateCourseDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
