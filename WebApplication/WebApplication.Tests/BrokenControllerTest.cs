using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Clients;
using Xunit;

namespace WebApplication.Tests
{
    public class BrokenControllerTest : BaseTest
    {
        [Fact]
        public async Task ShouldFail()
        {
            var brokenClient = new BrokenClient(BASE_URI, Client);
            var ex = await Assert.ThrowsAsync<ApiException<ProblemDetails>>(() => brokenClient.GetAsync());
            ex.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
    }
}