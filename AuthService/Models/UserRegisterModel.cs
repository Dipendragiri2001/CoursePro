namespace AuthService.Models
{
    public class UserRegisterModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } = string.Empty;
        public List<UserRole> Roles { get; set; } = new List<UserRole>();
    }

    public enum UserRole
    {
        SuperAdmin,
        Admin,
        Student
    }
}
