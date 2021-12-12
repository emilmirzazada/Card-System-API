using AutoMapper;
using CardSystem.Application.Cards.Commands.CreateCard;
using CardSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CardSystem.Application.Common.Mappings
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {
            CreateMap<CreateCardCommand, Card>().ReverseMap();

            /*CreateMap<Service, GetAllServicesViewModel>()
                .ForMember(vm => vm.ClientName, ex => ex.MapFrom((m, vm) =>
                {
                    return m.User?.FullName;
                }));*/
        }
    }
}
