﻿namespace CoursePro.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string Role { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
