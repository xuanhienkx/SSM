using AutoMapper;

namespace SSM.DomainProfiles
{
    public class AutoMapperDomainConfiguration
    {
        public static void Config()
        {
             Mapper.AddProfile(new TradingProfile());
             Mapper.AddProfile(new DataEntryProfile());
        }
    }
}