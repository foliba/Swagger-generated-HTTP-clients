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
            var res = await _weatherForecastClient.GetAsync(null);
            res.Should().NotBeNull();
            res.Count.Should().BePositive();
        }

        [Fact]
        public async Task ShouldReturnCorrectAmountOfElements()
        {
            var expectedAmount = 10;
            var res = await _weatherForecastClient.GetAsync(expectedAmount);
            res.Count.Should().Be(expectedAmount);
        }
        
        [Fact]
        public async Task ShouldReturnDefaultFiveElements()
        {
            var weatherForecastClient = new WeatherForecastClient(BASE_URI, Client);
            const int expectedAmount = 5;
            var res = await weatherForecastClient.GetAsync(amount: null);
            res.Count.Should().Be(expectedAmount);
        }
    }
}