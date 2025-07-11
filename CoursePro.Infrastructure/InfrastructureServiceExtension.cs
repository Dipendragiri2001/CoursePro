using CoursePro.Application.Services.Contracts;
using CoursePro.Domain.Contracts;
using CoursePro.Infrastructure.Repositories;
using CoursePro.Infrastructure.Services.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoursePro.Infrastructure
{
    public static class InfrastructureServiceExtension
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICourseService, CourseService>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
