using CardSystem.Application.Common.Interfaces;
using CardSystem.Domain.Entities;
using CardSystem.Domain.Enums;
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
    public class GetCardListQuery : IRequest<List<Card>>
    {
        public string Flag { get; set; }
        public class GetCardListQueryHandler : IRequestHandler<GetCardListQuery, List<Card>>
        {
            private readonly ICardSystemDbContext _context;

            public GetCardListQueryHandler(ICardSystemDbContext context)
            {
                _context = context;
            }

            public async Task<List<Card>> Handle(GetCardListQuery request, CancellationToken cancellationToken)
            {
                if (request.Flag=="1")
                {
                    var clientCards = _context.ClientCards
                                  .AsEnumerable();

                    var cards = _context.Cards
                                    .Where(r => !clientCards.Select(x => x.CardId).Contains(r.Id))
                                    .ToList();

                    return await Task.FromResult(cards);
                }
                if (request.Flag == "2")
                {
                    var clientCards = _context.ClientCards
                                  .AsEnumerable();

                    var cards = _context.Cards
                                    .ToList();

                    foreach (var card in cards)
                    {
                        card.State = (CardState)(clientCards.Any(x => x.CardId==card.Id)?0:1);
                    }

                    return await Task.FromResult(cards);
                }
                else
                {
                    var clientCards = _context.ClientCards
                                  .Where(x=>x.ClientId==request.Flag)
                                  .ToList();

                    var cards = _context.Cards
                                    .Where(r => clientCards.Select(x => x.CardId).Contains(r.Id))
                                    .ToList();

                    return await Task.FromResult(cards);
                }

            }
        }
    }
}
