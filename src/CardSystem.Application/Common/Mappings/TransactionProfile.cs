using AutoMapper;
using CardSystem.Application.Transactions.Commands.CreateDeposit;
using CardSystem.Application.Transactions.Commands.WithdrawFund;
using CardSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSystem.Application.Common.Mappings
{
    public class TransactionProfile:Profile
    {
        public TransactionProfile()
        {
            CreateMap<CreateDepositCommand, Transaction>().ReverseMap();

            CreateMap<WithdrawFundCommand, Transaction>().ReverseMap();
        }
    }
}
