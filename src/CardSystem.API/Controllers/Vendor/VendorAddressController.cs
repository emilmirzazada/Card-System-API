using CardSystem.Application.VendorAddresses.Commands.AddVendorAddress;
using CardSystem.Application.VendorAddresses.Queries.GetVendorAddressList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardSystem.API.Controllers.Vendor
{
    public class VendorAddressController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get(int vendorId)
        {
            var vendors = await Mediator.Send(new GetVendorAddressListQuery { VendorId = vendorId });
            return Ok(vendors);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create(AddVendorAddressCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
