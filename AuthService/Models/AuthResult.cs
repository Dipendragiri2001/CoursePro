using System.Security.Claims;

namespace AuthService.Models
{
    public class AuthResult
    {
        public bool Succeeded { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public string? ErrorMessage { get; set; }

        public List<string> Roles { get; set; } = new List<string>();
    }
}
