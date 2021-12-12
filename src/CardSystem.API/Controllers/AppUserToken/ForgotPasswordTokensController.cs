using CardSystem.Application.Common.Interfaces;
using CardSystem.Application.DTOs.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardSystem.API.Controllers.AppUserToken
{
    public class ForgotPasswordTokensController : BaseController
    {
        private readonly IAppUserService _appUserService;

        public ForgotPasswordTokensController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SendToken(ForgotPasswordRequest model)
        {
           await _appUserService.ForgotPassword(model);


            return Ok();
        }
    }
}
