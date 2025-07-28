using AuthService;
using AuthService.Models;
using CoursePro.API.Mappings;
using CoursePro.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddAutoMapper(typeof(CourseProMappings));

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", opt =>
    {
        opt.AllowAnyMethod();
        opt.AllowAnyHeader();
        opt.WithOrigins("http://localhost:5174");
        opt.AllowCredentials();

    });
});
var app = builder.Build();

//fetched from UserSecrets
List<UserRegisterModel> userSeeds = builder.Configuration.GetSection("UserSeeds").Get<List<UserRegisterModel>>() ?? new List<UserRegisterModel>();
if(userSeeds.Count > 0)
{
    //Seed Database with initial users
    await app.SeedDbAsync(seedUsers => seedUsers.AddRange(userSeeds));
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyCorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
