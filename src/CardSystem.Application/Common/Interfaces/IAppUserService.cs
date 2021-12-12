using CardSystem.Application.DTOs.Account;
using CardSystem.Application.DTOs.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSystem.Application.Common.Interfaces
{
    public interface IAppUserService
    {
        Task Logout();
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task RegisterAsync(RegisterRequest request);
        Task ForgotPassword(ForgotPasswordRequest model);
        Task<string> ResetPassword(ResetPasswordRequest model);
        Task<JwtTokenDto> RevokeByRefreshToken(string token);
    }
}
