using CardSystem.Application.Common.Interfaces;
using CardSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CardSystem.Application.Accounts.Queries.GetAccountList
{
    public class GetAccountListQuery : IRequest<IEnumerable<Account>>
    {
        public int Flag { get; set; }
        public class GetAccountListQueryHandler : IRequestHandler<GetAccountListQuery, IEnumerable<Account>>
        {
            private readonly ICardSystemDbContext _context;

            public GetAccountListQueryHandler(ICardSystemDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Account>> Handle(GetAccountListQuery request, CancellationToken cancellationToken)
            {
                if (request.Flag==1)
                {
                    var clientCards = _context.ClientCards
                                    .AsEnumerable();

                    var users = _context.Accounts
                                    .Where(r => !clientCards.Select(x => x.AccountId).Contains(r.Id));

                    return await Task.FromResult(users);
                }
                else
                {
                    var users = _context.Accounts
                                    .AsEnumerable();

                    return await Task.FromResult(users);
                }

            }
        }
    }
}
