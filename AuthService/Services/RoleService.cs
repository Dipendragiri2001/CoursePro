using AuthService.Entities;
using AuthService.Interfaces;
using AuthService.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Services
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public RoleService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<string>> GetAllRolesAsync()
        {
            return await Task.FromResult(
                _roleManager.Roles.Select(r => r.Name!).ToList()
            );
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName)) return true;

            var result = await _roleManager.CreateAsync(new IdentityRole<Guid> { Name = roleName });
            return result.Succeeded;
        }

        public async Task<bool> AssignRolesToUserAsync(string username, List<UserRole> roles)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return false;

            var roleStrings = roles.Select(r => r.ToString());
            var result = await _userManager.AddToRolesAsync(user, roleStrings);
            return result.Succeeded;
        }

        public async Task<bool> RemoveRolesFromUserAsync(string username, List<UserRole> roles)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return false;

            var roleStrings = roles.Select(r => r.ToString());
            var result = await _userManager.RemoveFromRolesAsync(user, roleStrings);
            return result.Succeeded;
        }

        public async Task<List<UserRole>> GetRolesByUserAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return new List<UserRole>();

            var roleNames = await _userManager.GetRolesAsync(user);
            return roleNames
                .Where(name => Enum.TryParse<UserRole>(name, out _))
                .Select(name => Enum.Parse<UserRole>(name))
                .ToList();
        }

    }
}
