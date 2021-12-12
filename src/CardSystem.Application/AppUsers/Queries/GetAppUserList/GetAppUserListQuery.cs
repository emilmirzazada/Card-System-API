using CardSystem.Application.Common.Interfaces;
using CardSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CardSystem.Application.AppUsers.Queries.GetAppUserList
{
    public class GetAppUserListQuery : IRequest<IEnumerable<User>>
    {
        public class GetAppUserListQueryHandler : IRequestHandler<GetAppUserListQuery, IEnumerable<User>>
        {
            private readonly ICardSystemDbContext _context;

            public GetAppUserListQueryHandler(ICardSystemDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<User>> Handle(GetAppUserListQuery request, CancellationToken cancellationToken)
            {
                var users = _context.Users
                                    .AsEnumerable();


                return await Task.FromResult(users);

            }
        }
    }
}
