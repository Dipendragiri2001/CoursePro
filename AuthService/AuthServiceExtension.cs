using AuthService.Data;
using AuthService.Entities;
using AuthService.Models;
using AuthService.Services;
using AuthService.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Reflection;
using System.Text;

namespace AuthService
{
    public static class AuthServiceExtension
    {
        public static void AddAuthServices(this IServiceCollection services,
            IConfigurationManager config
            )
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"), opts =>
                {
                    opts.CommandTimeout(60); // Set command timeout to 60 seconds
                });

            });

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            IConfigurationSection jwtConfigSection = config.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtConfigSection); //configure so that JwtSettings can be injected into controllers or services as IOptions<JwtSettings>

            JwtSettings? jwtSettings = jwtConfigSection.Get<JwtSettings>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings?.Issuer,
                    ValidAudience = jwtSettings?.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.SecretKey ?? String.Empty))
                };
            });

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }

        public static async Task SeedDbAsync(this WebApplication app, Action<List<UserRegisterModel>> registerUser)
        {
            using (var scope = app.Services.CreateScope())
            {
                var authDbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();

                if (await authDbContext.Users.FirstOrDefaultAsync() is not null)
                    return;

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

                var executionStrategy = authDbContext.Database.CreateExecutionStrategy();

                await executionStrategy.ExecuteAsync(async () =>
                {
                    //Begin Transaction
                    using (var transaction = await authDbContext.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            List<UserRegisterModel> userSeeds = new List<UserRegisterModel>();
                            registerUser.Invoke(userSeeds);


                            List<string?> roles = userSeeds.SelectMany(c => c.Roles.Select(x=>x.ToString())).Distinct()
                            .Except(roleManager.Roles.Select(c => c.Name).ToList()).ToList();

                            foreach (var role in roles)
                            {
                                if (!(await roleManager.CreateAsync(new IdentityRole<Guid> { Name = role.ToString() })).Succeeded)
                                {
                                    throw new Exception($"Failed to create role {role.ToString()}");
                                }
                            }

                            foreach (var seed in userSeeds)
                            {
                                ApplicationUser user = new ApplicationUser
                                {
                                    FullName = seed.FullName,
                                    Email = seed.FullName,
                                    UserName = seed.UserName
                                };

                                if (!(await userManager.CreateAsync(user, seed.Password)).Succeeded)
                                {
                                    throw new Exception("Failed to create an User");
                                }

                                if (!(await userManager.AddToRolesAsync(user, seed.Roles.Select(c => c.ToString()))).Succeeded)
                                {
                                    throw new Exception("Failed to assign user roles");
                                }
                            }

                            await transaction.CommitAsync();
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            throw new Exception(ex.Message);
                        }
                    }
                });

            }
        }
    }
}
