using CardSystem.API.Extensions;
using CardSystem.Application.Transactions.Queries.GetTransactionList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardSystem.API.Controllers.Transaction
{
    public class TransactionController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllTransactions(string userId)
        {
            var userTransactions = await Mediator.Send(new GetTransactionListQuery { UserId = userId });
            return Ok(userTransactions);
        }

    }
}
