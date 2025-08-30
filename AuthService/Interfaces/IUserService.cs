using AuthService.Entities;
using AuthService.Models;

namespace AuthService.Interfaces
{
    public interface IUserService
    {
        Task<UserRegisterModel?> GetByUsernameAsync(string username);
        Task<IEnumerable<UserRegisterModel>> GetAllAsync();
        Task<bool> CreateAsync(ApplicationUser user, string password);
        Task<bool> DeleteByUsernameAsync(string username);
        Task<bool> UpdateRolesAsync(string username, List<UserRole> roles);
    }
}
