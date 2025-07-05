using CoursePro.Domain.Contracts;
using CoursePro.Infrastructure.Context;
using CoursePro.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoursePro.Infrastructure
{
    public static class InfrastructureServiceExtension
    {
        public static void AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
