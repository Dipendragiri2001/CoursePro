using AuthService.Models;

namespace AuthService.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthResult> LoginAsync(UserLoginModel model);
    }
}
