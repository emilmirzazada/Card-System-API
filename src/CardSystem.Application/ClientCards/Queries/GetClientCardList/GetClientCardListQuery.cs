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

namespace CardSystem.Application.ClientCards.Queries.GetClientCardList
{
    public class GetClientCardListQuery : IRequest<IEnumerable<ClientCard>>
    {
        public string UserId { get; set; }
        public class GetClientCardListQueryHandler : IRequestHandler<GetClientCardListQuery, IEnumerable<ClientCard>>
        {
            private readonly ICardSystemDbContext _context;

            public GetClientCardListQueryHandler(ICardSystemDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<ClientCard>> Handle(GetClientCardListQuery request, CancellationToken cancellationToken)
            {
                if (request.UserId == null)
                {
                    var cards = _context.ClientCards
                                    .Include(x => x.Client)
                                    .Include(x => x.Card)
                                    .Include(x => x.Account)
                                    .AsEnumerable();
                    return await Task.FromResult(cards);
                }
                else
                {
                    var cards =  _context.ClientCards
                                     .Include(x => x.Client)
                                     .Where(i => i.ClientId == request.UserId)
                                     .Include(x => x.Card)
                                     .Include(x => x.Account)
                                     .AsEnumerable();
                    return await Task.FromResult(cards);
                }

                

            }
        }
    }
}
