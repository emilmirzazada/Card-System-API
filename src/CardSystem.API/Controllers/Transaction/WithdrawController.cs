using CardSystem.API.Extensions;
using CardSystem.Application.Transactions.Commands.WithdrawFund;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardSystem.API.Controllers.Transaction
{
    public class WithdrawController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Withdraw(WithdrawFundCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
