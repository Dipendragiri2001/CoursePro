using CoursePro.Application.Mappings;
using CoursePro.Application.Services;
using CoursePro.Application.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace CoursePro.Application
{
    public static class ApplicationServiceExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<ICourseService, CourseService>();
        }
    }
}
