namespace CoursePro.Domain.Entities
{
    public class Section
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}
