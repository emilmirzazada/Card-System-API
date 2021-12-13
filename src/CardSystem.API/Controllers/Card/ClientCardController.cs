using CardSystem.API.Extensions;
using CardSystem.Application.ClientCards.Commands.CreateClientCard;
using CardSystem.Application.ClientCards.Queries.GetClientCardList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardSystem.API.Controllers.Card
{
    public class ClientCardController : BaseController
    {
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create(CreateClientCardCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var clientCards = await Mediator.Send(new GetClientCardListQuery { UserId = userId });
                return Ok(clientCards);
            }
            else
            {
                var clientCards = await Mediator.Send(new GetClientCardListQuery());
                return Ok(clientCards);
            }
           
        }
    }
}
