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

namespace CardSystem.Application.Vendors.Commands.CreateVendor
{
    public class CreateVendorCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public class CreateVendorCommandHandler : IRequestHandler<CreateVendorCommand, int>
        {
            private readonly ICardSystemDbContext _context;
            private readonly IMapper _mapper;

            public CreateVendorCommandHandler(ICardSystemDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
            {
                var entity = new Vendor
                {
                    Name = request.Name,
                    Phone = request.Phone,
                    Email = request.Email
                };

                _context.Vendors.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
