using CardSystem.Application.Common.Interfaces;
using CardSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CardSystem.Application.AppUsers.Queries.GetAppUserById
{
    public class GetAppUserByIdQuery : IRequest<User>
    {
        public string Id { get; set; }
        public class GetAppUserListQueryHandler : IRequestHandler<GetAppUserByIdQuery, User>
        {
            private readonly UserManager<User> userManager;

            public GetAppUserListQueryHandler(UserManager<User> userManager)
            {
                this.userManager = userManager;
            }

            public async Task<User> Handle(GetAppUserByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByIdAsync(request.Id);

                return user;

            }
        }
    }
}
