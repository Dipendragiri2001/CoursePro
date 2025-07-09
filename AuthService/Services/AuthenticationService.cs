using AuthService.Entities;
using AuthService.Models;
using AuthService.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtOptions, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtOptions.Value;
            _roleManager = roleManager;
        }

        public async Task<AuthResult> LoginAsync(UserLoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return new AuthResult
                {
                    Succeeded = false,
                    ErrorMessage = "Invalid username or password"
                };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                return new AuthResult
                {
                    Succeeded = false,
                    ErrorMessage = "Invalid username or password"
                };
            }

            //update last login
            user.LastLogin = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return new AuthResult
            {
                Token = await GenerateTokenAsync(user),
                ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes)
            };
        }

        private async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = (await _userManager.GetClaimsAsync(user)).ToList();

            List<string> roles = (List<string>)await _userManager.GetRolesAsync(user);

            IList<Claim> roleClaims;
            foreach (var role in roles)
            {
                var foundRole = await _roleManager.FindByNameAsync(role);
                roleClaims = await _roleManager.GetClaimsAsync(foundRole);
                claims.AddRange(roleClaims);
            }

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));


            var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
