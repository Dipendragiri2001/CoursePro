namespace CoursePro.Application.DTOs.Courses
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public Guid CreatedById { get; set; }
    }

    public class CreateCourseDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public Guid CreatedById { get; set; }
    }

    public class UpdateCourseDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
    }

}
