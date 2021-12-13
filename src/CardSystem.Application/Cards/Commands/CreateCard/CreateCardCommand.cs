using AutoMapper;
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

namespace CardSystem.Application.Cards.Commands.CreateCard
{
    public class CreateCardCommand : IRequest<int>
    {
        public string Number { get; set; }

        public string CVV { get; set; }
        public bool Valid { get; set; }
        [EnumDataType(typeof(CardType))]
        [JsonConverter(typeof(StringEnumConverter))]
        public CardType Type { get; set; }

        public DateTime ExpirationDate { get; set; }


        public class CreateCardCommandHandler : IRequestHandler<CreateCardCommand, int>
        {
            private readonly ICardSystemDbContext _context;
            private readonly IMapper _mapper;
            private readonly IDateTimeService _dateTimeService;

            public CreateCardCommandHandler(ICardSystemDbContext context, IMapper mapper,
                IDateTimeService dateTimeService)
            {
                _context = context;
                _mapper = mapper;
                _dateTimeService = dateTimeService;
            }

            public async Task<int> Handle(CreateCardCommand request, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<Card>(request);

                entity.DateRegistered = _dateTimeService.Now;

                _context.Cards.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
