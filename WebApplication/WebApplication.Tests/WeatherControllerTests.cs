using System.Threading.Tasks;
using FluentAssertions;
using WebApplication.Clients;
using Xunit;

namespace WebApplication.Tests
{
    public class WeatherControllerTests : BaseTest
    {
        private readonly WeatherForecastClient _weatherForecastClient;

        public WeatherControllerTests()
        {
            _weatherForecastClient = new WeatherForecastClient(BASE_URI, Client);
        }

        [Fact]
        public async Task ShouldReturnSuccess()
        {
            var res = await _weatherForecastClient.GetAsync();
            res.Should().NotBeNull();
            res.Count.Should().BePositive();
        }
    }
}