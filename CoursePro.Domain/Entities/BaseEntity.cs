namespace CoursePro.Domain.Entities
{
    public class BaseEntity<T> : IBaseEntity<T>
    {
        public T Id { get; set; }
        public Guid CreatedById { get; set; }
        public Guid UpdatedById { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;

    }
    public interface IBaseEntity<T>
    {
        T Id { get; set; }
        Guid CreatedById { get; set; }
        Guid UpdatedById { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime UpdatedOn { get; set; }
    }
}
