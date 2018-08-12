using AutoMapper;
using Bitcoin.Wallet.Api.Models;
using Bitcoin.Wallet.Api.Models.Request;

namespace Bitcoin.Wallet.Api.ProfileMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<BitcoinRequest, BitcoinQuery>()
               .ForMember(vm => vm.Address, map => map.MapFrom(m => m.Address))
               .ForMember(vm => vm.Amount, map => map.MapFrom(m => m.Amount));
        }
    }
}