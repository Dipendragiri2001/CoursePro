using CoursePro.Domain.Contracts;
using CoursePro.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CoursePro.Infrastructure
{
    public static class InfrastructureServiceExtension
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentRepository, StudentRepository>();
        }
    }
}
