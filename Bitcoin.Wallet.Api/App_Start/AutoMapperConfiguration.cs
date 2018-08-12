using AutoMapper;
using Bitcoin.Wallet.Api.ProfileMapper;

namespace Bitcoin.Wallet.Api.App_Start
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<MappingProfile>();
            });

            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}