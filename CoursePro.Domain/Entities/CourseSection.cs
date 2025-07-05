namespace CoursePro.Domain.Entities
{
    public class CourseSection : BaseEntity<Guid>
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}
