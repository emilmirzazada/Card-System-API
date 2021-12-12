using AutoMapper;
using CardSystem.Application.Common.Interfaces;
using CardSystem.Domain.Entities;
using CardSystem.Domain.Enums;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CardSystem.Application.VendorAddresses.Commands.AddVendorAddress
{
    public class AddVendorAddressCommand : IRequest<int>
    {
        public int VendorId { get; set; }
        public string Address { get; set; }

        public class AddVendorAddressCommandHandler : IRequestHandler<AddVendorAddressCommand, int>
        {
            private readonly ICardSystemDbContext _context;
            private readonly IMapper _mapper;

            public AddVendorAddressCommandHandler(ICardSystemDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(AddVendorAddressCommand request, CancellationToken cancellationToken)
            {
                var entity = new VendorAddress
                {
                    VendorId = request.VendorId,
                    Address = request.Address
                };

                _context.VendorAddresses.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
