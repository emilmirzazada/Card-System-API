using CardSystem.API.Extensions;
using CardSystem.Application.Vendors.Commands.CreateVendor;
using CardSystem.Application.Vendors.Queries.GetVendorList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardSystem.API.Controllers.Vendor
{
   
    public class VendorController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var vendors = await Mediator.Send(new GetVendorListQuery());
            return Ok(vendors);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create(CreateVendorCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
