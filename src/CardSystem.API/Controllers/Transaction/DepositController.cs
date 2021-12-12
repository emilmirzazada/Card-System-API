using CardSystem.API.Extensions;
using CardSystem.Application.Transactions.Commands.CreateDeposit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardSystem.API.Controllers.Transaction
{
    public class DepositController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create(CreateDepositCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
