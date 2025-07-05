using CoursePro.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace CoursePro.Application
{
    public static class ApplicationServiceExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}
