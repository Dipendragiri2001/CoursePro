namespace CoursePro.Domain.Entities
{
    public class Enrollment : BaseEntity<long>
    {
        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
        public Course Course { get; set; }
        public User User { get; set; }
    }
}
