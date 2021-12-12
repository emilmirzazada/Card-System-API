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

namespace CardSystem.Application.Transactions.Queries.GetTransactionList
{
    public class GetTransactionListQuery : IRequest<List<Transaction>>
    {
        public string UserId { get; set; }
        public class GetTransactionListQueryHandler : IRequestHandler<GetTransactionListQuery, List<Transaction>>
        {
            private readonly ICardSystemDbContext _context;

            public GetTransactionListQueryHandler(ICardSystemDbContext context)
            {
                _context = context;
            }

            public async Task<List<Transaction>> Handle(GetTransactionListQuery request, CancellationToken cancellationToken)
            {
                var transactions = _context.Transactions
                                    .Where(i => i.ClientId == request.UserId)
                                    .Include(x => x.Client)
                                    .Include(x=>x.Vendor)
                                    .Include(x=>x.Card)
                                    .ToList();


                return await Task.FromResult(transactions);

            }
        }
    }
}
