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
        public class GetAccountListQueryHandler : IRequestHandler<GetAccountListQuery, IEnumerable<Account>>
        {
            private readonly ICardSystemDbContext _context;

            public GetAccountListQueryHandler(ICardSystemDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Account>> Handle(GetAccountListQuery request, CancellationToken cancellationToken)
            {
                var users = _context.Accounts
                                    .AsEnumerable();

                return await Task.FromResult(users);

            }
        }
    }
}
