using CardSystem.API.Extensions;
using CardSystem.Application.Accounts.Commands.CreateAccount;
using CardSystem.Application.Accounts.Queries.GetAccountList;
using CardSystem.Application.AppUsers.Commands.EditAppUser;
using CardSystem.Application.Common.Interfaces;
using CardSystem.Application.DTOs.Account;
using CardSystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardSystem.API.Controllers.Account
{
    public class AccountController : BaseController
    {
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create(CreateAccountCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Get(int flag)
        {
            var accounts = await Mediator.Send(new GetAccountListQuery { Flag=flag});
            return Ok(accounts);
        }
    }
}
