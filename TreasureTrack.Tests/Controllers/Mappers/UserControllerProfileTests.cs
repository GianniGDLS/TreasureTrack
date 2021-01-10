using AutoMapper;
using TreasureTrack.Controllers.Mappers;
using Xunit;

namespace TreasureTrack.Tests.Controllers.Mappers
{
    public class UserControllerProfileTests
    {
       [Fact]
       public void CheckUserControllerProfileValid()
        {
            var config = new MapperConfiguration(opts => opts.AddProfile(new UserControllerProfile()));
            var mapper = config.CreateMapper();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}