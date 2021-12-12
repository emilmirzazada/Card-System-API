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

namespace CardSystem.Application.Cards.Queries.GetCardList
{
    public class GetCardListQuery : IRequest<IEnumerable<Card>>
    {
        public class GetCardListQueryHandler : IRequestHandler<GetCardListQuery, IEnumerable<Card>>
        {
            private readonly ICardSystemDbContext _context;

            public GetCardListQueryHandler(ICardSystemDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Card>> Handle(GetCardListQuery request, CancellationToken cancellationToken)
            {
                var cards = _context.Cards
                                    .AsEnumerable();

                return await Task.FromResult(cards);

            }
        }
    }
}
