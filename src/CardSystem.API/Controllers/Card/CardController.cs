using CardSystem.API.Extensions;
using CardSystem.Application.Cards.Commands.CreateCard;
using CardSystem.Application.Cards.Queries.GetCardById;
using CardSystem.Application.Cards.Queries.GetCardList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardSystem.API.Controllers.Card
{
    public class CardController : BaseController
    {
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create(CreateCardCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet()]
        public async Task<IActionResult> GetAllCards(string flag)
        {
            var clientCards = await Mediator.Send(new GetCardListQuery {Flag=flag });
            return Ok(clientCards);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cards = await Mediator.Send(new GetCardByIdQuery { Id=id});
            return Ok(cards);
        }
    }
}
