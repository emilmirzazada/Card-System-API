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


namespace CardSystem.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest<int>
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }

        public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, int>
        {
            private readonly ICardSystemDbContext _context;
            private readonly IMapper _mapper;

            public CreateAccountCommandHandler(ICardSystemDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<Account>(request);

                _context.Accounts.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
