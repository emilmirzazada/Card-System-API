using CardSystem.Application.Common.Interfaces;
using CardSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CardSystem.Application.Cards.Queries.GetCardById
{
    public class GetCardByIdQuery : IRequest<Card>
    {
        public int Id { get; set; }
        public class GetCardByIdQueryHandler : IRequestHandler<GetCardByIdQuery, Card>
        {
            private readonly ICardSystemDbContext _context;

            public GetCardByIdQueryHandler(ICardSystemDbContext context)
            {
                _context = context;
            }

            public async Task<Card> Handle(GetCardByIdQuery request, CancellationToken cancellationToken)
            {
                var cards = _context.Cards
                                    .FirstOrDefault(x=>x.Id==request.Id);

                return await Task.FromResult(cards);

            }
        }
    }
}
