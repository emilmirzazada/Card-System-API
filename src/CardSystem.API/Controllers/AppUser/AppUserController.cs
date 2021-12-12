using CardSystem.API.Extensions;
using CardSystem.Application.AppUsers.Commands.EditAppUser;
using CardSystem.Application.AppUsers.Queries.GetAppUserById;
using CardSystem.Application.AppUsers.Queries.GetAppUserList;
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
    public class AppUserController : BaseController
    {
        private readonly IAppUserService _appUserService;

        public AppUserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            await _appUserService.RegisterAsync(request);
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> Update(EditAppUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var userTransactions = await Mediator.Send(new GetAppUserListQuery());
            return Ok(userTransactions);
        }
        [HttpGet("GetUserById")]
        public async Task<IActionResult> Get(string userId)
        {
            var userTransactions = await Mediator.Send(new GetAppUserByIdQuery { Id=userId});
            return Ok(userTransactions);
        }
    }
}
