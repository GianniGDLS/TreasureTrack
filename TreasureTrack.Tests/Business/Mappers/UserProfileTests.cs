using AutoMapper;
using TreasureTrack.Business.Mappers;
using Xunit;

namespace TreasureTrack.Tests.Business.Mappers
{
    public class UserProfileTests
    {
        [Fact]
        public void CheckUserProfileValid()
        {
            var config = new MapperConfiguration(opts => opts.AddProfile(new UserProfile()));
            var mapper = config.CreateMapper();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}