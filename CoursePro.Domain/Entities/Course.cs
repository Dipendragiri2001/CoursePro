using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePro.Domain.Entities
{
    public class Course
    {
        public Guid Id { get; set; }
        public  string? Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public User CreatedBy { get; set; }
    }
}
