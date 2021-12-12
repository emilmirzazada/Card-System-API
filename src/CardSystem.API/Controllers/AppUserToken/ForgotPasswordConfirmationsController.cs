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
    public class ForgotPasswordConfirmationsController : BaseController
    {
        private readonly IAppUserService _appUserService;

        public ForgotPasswordConfirmationsController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            var result = await _appUserService.ResetPassword(model);

            /*return Ok(await _appUserService.AuthenticateAsync(new AuthenticationRequest
            {
                Email = model.Email,
                Password = model.Password
            }));*/

            return Ok(result);
        }
    }
}
