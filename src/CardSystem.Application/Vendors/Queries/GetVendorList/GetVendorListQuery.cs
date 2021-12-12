using CardSystem.Application.Common.Interfaces;
using CardSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CardSystem.Application.Vendors.Queries.GetVendorList
{
    public class GetVendorListQuery : IRequest<IEnumerable<Vendor>>
    {
        public class GetVendorListQueryHandler : IRequestHandler<GetVendorListQuery, IEnumerable<Vendor>>
        {
            private readonly ICardSystemDbContext _context;

            public GetVendorListQueryHandler(ICardSystemDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Vendor>> Handle(GetVendorListQuery request, CancellationToken cancellationToken)
            {
                var vendors = _context.Vendors
                                    .AsEnumerable();

                return await Task.FromResult(vendors);

            }
        }
    }
}
