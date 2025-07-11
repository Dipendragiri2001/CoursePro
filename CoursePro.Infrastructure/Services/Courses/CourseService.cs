using AutoMapper;
using CoursePro.Application.DTOs.Courses;
using CoursePro.Application.Services.Contracts;
using CoursePro.Domain.Contracts;
using CoursePro.Domain.Entities;

namespace CoursePro.Infrastructure.Services.Courses
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course, Guid> _courseRepository;
        private readonly IMapper _mapper;

        public CourseService(IRepository<Course, Guid> courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<CourseDto> CreateAsync(CreateCourseDto dto)
        {
            var course = _mapper.Map<Course>(dto);
            course.Id = Guid.NewGuid();
            course.CreatedOn = DateTime.UtcNow;

            await _courseRepository.AddAsync(course);
            return _mapper.Map<CourseDto>(course);
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            var courses = await _courseRepository.GetAsync();
            return _mapper.Map<List<CourseDto>>(courses);
        }

        public async Task<CourseDto?> GetByIdAsync(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            return _mapper.Map<CourseDto?>(course);
        }

        public async Task<bool> UpdateAsync(UpdateCourseDto dto)
        {
            var course = await _courseRepository.GetByIdAsync(dto.Id);
            if (course == null) return false;

            _mapper.Map(dto, course);
            await _courseRepository.UpdateAsync(course);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return false;

            await _courseRepository.DeleteAsync(course);
            return true;
        }

    }
}
