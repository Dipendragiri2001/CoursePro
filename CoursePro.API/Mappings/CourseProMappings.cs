using AuthService.Models;
using AutoMapper;
using CoursePro.API.Models;

namespace CoursePro.API.Mappings
{

    public class CourseProMappings: Profile
    {
        public CourseProMappings()
        {
            CreateMap<LoginModel, UserLoginModel>();
            CreateMap<AuthResult, AuthResultModel>();
        }
    }
}
