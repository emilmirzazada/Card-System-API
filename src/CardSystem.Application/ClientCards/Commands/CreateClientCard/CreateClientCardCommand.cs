using AutoMapper;
using CardSystem.Application.Common.Interfaces;
using CardSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CardSystem.Application.ClientCards.Commands.CreateClientCard
{
    public class CreateClientCardCommand : IRequest<int>
    {
        public string ClientId { get; set; }
        public int AccountId { get; set; }
        public int CardId { get; set; }

        public class CreateClientCardCommandHandler : IRequestHandler<CreateClientCardCommand, int>
        {
            private readonly ICardSystemDbContext _context;
            private readonly IMapper _mapper;

            public CreateClientCardCommandHandler(ICardSystemDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(CreateClientCardCommand request, CancellationToken cancellationToken)
            {
                if (_context.ClientCards.Any(x => x.AccountId == request.AccountId))
                    throw new Exception("This account has already had a card");

                var entity = _mapper.Map<ClientCard>(request);

                _context.ClientCards.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
