using CardSystem.Application.Common.Interfaces;
using CardSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CardSystem.Application.VendorAddresses.Queries.GetVendorAddressList
{
    public class GetVendorAddressListQuery : IRequest<IEnumerable<VendorAddress>>
    {
        public int VendorId { get; set; }
        public class GetVendorAddressListQueryHandler : IRequestHandler<GetVendorAddressListQuery, IEnumerable<VendorAddress>>
        {
            private readonly ICardSystemDbContext _context;

            public GetVendorAddressListQueryHandler(ICardSystemDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<VendorAddress>> Handle(GetVendorAddressListQuery request, CancellationToken cancellationToken)
            {
                var vendors = _context.VendorAddresses
                                    .Where(x=>x.VendorId==request.VendorId)
                                    .AsEnumerable();

                return await Task.FromResult(vendors);

            }
        }
    }
}
