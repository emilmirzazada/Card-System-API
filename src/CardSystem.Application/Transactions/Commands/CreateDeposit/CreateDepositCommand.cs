﻿using AutoMapper;
using CardSystem.Application.Common.Interfaces;
using CardSystem.Domain.Entities;
using CardSystem.Domain.Enums;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CardSystem.Application.Transactions.Commands.CreateDeposit
{
    public class CreateDepositCommand : IRequest<int>
    {
        [Range(10.00, 10000.00)]
        public decimal Amount { get; set; }
        public int CardId { get; set; }
        public int VendorId { get; set; }
        [EnumDataType(typeof(TransactionType))]
        [JsonConverter(typeof(StringEnumConverter))]
        public TransactionType Type { get; set; }
        [EnumDataType(typeof(TransactionStatus))]
        [JsonConverter(typeof(StringEnumConverter))]
        public TransactionStatus Status { get; set; }

        public string ClientId { get; set; }

        public class CreateDepositCommandHandler : IRequestHandler<CreateDepositCommand, int>
        {
            private readonly ICardSystemDbContext _context;
            private readonly IMapper _mapper;
            private readonly IDateTimeService _dateTimeService;

            public CreateDepositCommandHandler(ICardSystemDbContext context, IMapper mapper,
                IDateTimeService dateTimeService)
            {
                _context = context;
                _mapper = mapper;
                _dateTimeService = dateTimeService;
            }

            public async Task<int> Handle(CreateDepositCommand request, CancellationToken cancellationToken)
            {
                var deposit = new Deposit
                {
                    Amount = request.Amount,
                    ClientId = request.ClientId,
                    CreatedAt = _dateTimeService.Now
                };
                _context.Deposits.Add(deposit);

                var entity = _mapper.Map<Transaction>(request);

                entity.CreatedAt = _dateTimeService.Now;

                _context.Transactions.Add(entity);

                var clientCard = _context.ClientCards.Where(x => x.CardId == request.CardId)!.FirstOrDefault();

                var account = _context.Accounts.Where(x => x.Id == clientCard.AccountId)!.FirstOrDefault();

                account.Balance += request.Amount;
                _context.Accounts.Update(account);

                await _context.SaveChangesAsync(cancellationToken);

                return deposit.Id;
            }
        }
    }
}
