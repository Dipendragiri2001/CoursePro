using AuthService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthResult> LoginAsync(UserLoginModel model);
    }
}
