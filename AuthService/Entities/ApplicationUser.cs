using Microsoft.AspNetCore.Identity;

namespace AuthService.Entities
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string? FullName { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
