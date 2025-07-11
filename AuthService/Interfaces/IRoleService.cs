using AuthService.Models;

namespace AuthService.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<string>> GetAllRolesAsync();
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> AssignRolesToUserAsync(string username, List<UserRole> roles);
        Task<bool> RemoveRolesFromUserAsync(string username, List<UserRole> roles);
        Task<List<UserRole>> GetRolesByUserAsync(string username);
    }
}
