using AutoMapper;
using CoursePro.Application.DTOs.Courses;
using CoursePro.Domain.Entities;

namespace CoursePro.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Course, CreateCourseDto>().ReverseMap();
            CreateMap<Course, UpdateCourseDto>().ReverseMap();
        }
    }
}
