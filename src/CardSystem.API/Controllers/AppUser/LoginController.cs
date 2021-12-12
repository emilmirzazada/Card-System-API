using CardSystem.Application.Common.Interfaces;
using CardSystem.Application.DTOs.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardSystem.API.Controllers.AppUser
{
    public class LoginController : BaseController
    {
        private readonly IAppUserService _appUserService;

        public LoginController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _appUserService.AuthenticateAsync(request));
        }
        [AllowAnonymous]
        [HttpGet("/api/Logout")]
        public async Task<IActionResult> Logout()
        {
            await _appUserService.Logout();
            return Ok();
        }
    }
}
