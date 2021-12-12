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

namespace CardSystem.Application.AppUsers.Commands.EditAppUser
{
    public class EditAppUserCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public class EditAppUserCommandHandler : IRequestHandler<EditAppUserCommand, Unit>
        {
            private readonly UserManager<User> userManager;
            private readonly IDateTimeService _dateTimeService;

            public EditAppUserCommandHandler(UserManager<User> userManager,IDateTimeService dateTimeService)
            {
                this.userManager = userManager;
                _dateTimeService = dateTimeService;
            }

            public async Task<Unit> Handle(EditAppUserCommand request, CancellationToken cancellationToken)
            {

                var user = await userManager.FindByIdAsync(request.UserId);
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Email = request.Email;
                user.UserName = request.Email;

               await  userManager.UpdateAsync(user);

                if (!string.IsNullOrEmpty(request.Password))
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    var result = await userManager.ResetPasswordAsync(user, token, request.Password);
                }


                return Unit.Value;
            }
        }
    }
}
