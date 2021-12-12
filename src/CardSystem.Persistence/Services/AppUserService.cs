using CardSystem.Application.Common.Interfaces;
using CardSystem.Application.DTOs.Account;
using CardSystem.Application.DTOs.Email;
using CardSystem.Application.DTOs.Jwt;
using CardSystem.Domain.Entities;
using CardSystem.Domain.Settings;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Cache;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CardSystem.Persistence.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailService _emailService;
        private readonly CardSystemDbContext context;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;
        public AppUserService(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<JWTSettings> jwtSettings,
            IDateTimeService dateTimeService,
            SignInManager<User> signInManager,
            IEmailService emailService,
            CardSystemDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _dateTimeService = dateTimeService;
            _signInManager = signInManager;
            this._emailService = emailService;
            this.context = context;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            try
            {
                var user = context.Users.
                            Include(x => x.RefreshToken)
                            .Where(x => x.Email == request.Email)
                            .FirstOrDefault();
                if (user == null)
                {
                    throw new Exception($"No Accounts Registered with {request.Email}.");
                }
                var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    throw new Exception($"Invalid Credentials for '{request.Email}'.");
                }

                user.LastLoginTime = _dateTimeService.Now;
                await _userManager.UpdateAsync(user);

                JwtTokenDto jwtTokenDto = await GenerateJWToken(user);
                AuthenticationResponse response = new AuthenticationResponse();
                response.Id = user.Id;
                response.Jwt = jwtTokenDto;
                response.Email = user.Email;
                response.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                response.Roles = rolesList.ToList();


                if (user.RefreshToken == null || user.RefreshToken.IsExpired)
                {
                    var refreshToken = GenerateRefreshToken();
                    user.RefreshToken = refreshToken;
                    context.Users.Update(user);
                    context.SaveChanges();
                    response.RefreshToken = new RefreshTokenDto(refreshToken.Token, refreshToken.Expires);
                }
                else
                {
                    response.RefreshToken = new RefreshTokenDto(user.RefreshToken.Token, user.RefreshToken.Expires);
                }

                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            var user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {

                    throw new Exception($"{result.Errors}");
                }
            }
            else
            {
                throw new Exception($"Email {request.Email } is already registered.");
            }
        }

        private async Task<JwtTokenDto> GenerateJWToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Id", user.Id)
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (Claim roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var expires = _dateTimeService.Now.AddMinutes(_jwtSettings.DurationInMinutes);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials);
            var tokentHandler = new JwtSecurityTokenHandler();
            string token = tokentHandler.WriteToken(jwtSecurityToken);
            return new JwtTokenDto(token, expires);
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = _dateTimeService.Now.AddMonths(6),
                Created = _dateTimeService.Now
            };
        }

        public async Task<JwtTokenDto> RevokeByRefreshToken(string token)
        {
            var refreshToken = context.RefreshTokens.Where(x => x.Token == token).FirstOrDefault();
            var user = context.Users.Where(x => x.RefreshTokenId == refreshToken.Id).FirstOrDefault();
            JwtTokenDto jwtTokenDto = await GenerateJWToken(user);
            return await Task.FromResult(new JwtTokenDto(jwtTokenDto.Token, jwtTokenDto.Expires));
        }

        public async Task ForgotPassword(ForgotPasswordRequest model)
        {
            var account = await _userManager.FindByEmailAsync(model.Email);

            if (account == null) throw new Exception($"No Accounts Registered with {model.Email}.");

            var code = await _userManager.GeneratePasswordResetTokenAsync(account);

            var url = $"https://cardsystem.vercel.app/recover_password?accessToken={code}";


            var emailRequest = new EmailRequest()
            {
                Body = $"Click the provided <a href='{url}'>link</a> to reset your password",
                To = model.Email,
                Subject = "Reset password",
            };
            await _emailService.SendAsync(emailRequest);

        }

        public async Task<string> ResetPassword(ResetPasswordRequest model)
        {
            try
            {
                var account = await _userManager.FindByEmailAsync(model.Email);
                if (account == null) throw new Exception($"No Accounts Registered with {model.Email}.");
                var result = await _userManager.ResetPasswordAsync(account, model.Token, model.Password);
                if (!result.Succeeded)
                {
                    throw new Exception($"Error occured while reseting the password.");
                }

                account.LastPasswordChangeDate = _dateTimeService.Now;
                await _userManager.UpdateAsync(account);

                return model.Token;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
