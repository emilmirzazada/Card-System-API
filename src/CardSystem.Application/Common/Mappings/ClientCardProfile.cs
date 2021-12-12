using AutoMapper;
using CardSystem.Application.ClientCards.Commands.CreateClientCard;
using CardSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSystem.Application.Common.Mappings
{
    public class ClientCardProfile:Profile
    {
        public ClientCardProfile()
        {
            CreateMap<CreateClientCardCommand, ClientCard>().ReverseMap();
        }
    }
}
