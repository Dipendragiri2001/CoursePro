namespace CoursePro.Domain.Entities
{
    public class Course : BaseEntity<Guid>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
    }
}
