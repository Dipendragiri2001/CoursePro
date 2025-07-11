using AuthService.Entities;
using AuthService.Interfaces;
using AuthService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<UserRegisterModel?> GetByUsernameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return null;

            var roleNames = await _userManager.GetRolesAsync(user);
            var roles = roleNames.Select(r => Enum.TryParse<UserRole>(r, out var ur) ? ur : UserRole.Student).ToList();

            return new UserRegisterModel
            {
                FullName = user.FullName,
                Email = user.Email ?? "",
                UserName = user.UserName ?? "",
                Roles = roles
            };
        }

        public async Task<IEnumerable<UserRegisterModel>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var result = new List<UserRegisterModel>();

            foreach (var user in users)
            {
                var roleNames = await _userManager.GetRolesAsync(user);
                var roles = roleNames.Select(r => Enum.TryParse<UserRole>(r, out var ur) ? ur : UserRole.Student).ToList();

                result.Add(new UserRegisterModel
                {
                    FullName = user.FullName,
                    Email = user.Email ?? "",
                    UserName = user.UserName ?? "",
                    Roles = roles
                });
            }

            return result;
        }

        public async Task<bool> UpdateRolesAsync(string username, List<UserRole> roles)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return false;

            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded) return false;

            var roleStrings = roles.Select(r => r.ToString());
            var addResult = await _userManager.AddToRolesAsync(user, roleStrings);
            return addResult.Succeeded;
        }

        public async Task<bool> DeleteByUsernameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

    }
}
