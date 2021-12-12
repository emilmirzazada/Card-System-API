using AutoMapper;
using CardSystem.Application.Accounts.Commands.CreateAccount;
using CardSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSystem.Application.Common.Mappings
{
    public class AccountProfile:Profile
    {
        public AccountProfile()
        {
            CreateMap<CreateAccountCommand, Account>().ReverseMap();
        }
    }
}
