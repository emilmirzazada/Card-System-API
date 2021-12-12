using CardSystem.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardSystem.API.Controllers.Enums
{
    public class EnumController : BaseController
    {
        [HttpGet("GetCardStates")]
        public IActionResult GetCardStates()
        {
            var cardStates = Enum.GetNames(typeof(CardState)).ToList();
            return Ok(cardStates);
        }
        [HttpGet("GetCardTypes")]
        public IActionResult GetCardTypes()
        {
            var cardStates = Enum.GetNames(typeof(CardType)).ToList();
            return Ok(cardStates);
        }
    }
}
