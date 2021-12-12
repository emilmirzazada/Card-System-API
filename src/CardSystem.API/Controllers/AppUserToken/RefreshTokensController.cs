using CardSystem.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardSystem.API.Controllers.AppUserToken
{
    public class RefreshTokensController : BaseController
    {
        private readonly IAppUserService _appUserService;

        public RefreshTokensController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Revoke([FromQuery] string refreshToken)
        {
            return Ok( await _appUserService.RevokeByRefreshToken(refreshToken));
        }
    }
}
