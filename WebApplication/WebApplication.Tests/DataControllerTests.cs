using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Clients;
using Xunit;

namespace WebApplication.Tests
{
    public class DataControllerTests : BaseTest
    {
        [Fact]
        public async Task ShouldReturnSameDateTime()
        {
            var dataClient = new DataClient(BASE_URI, Client);
            var currDate = DateTime.Parse("2021-06-24T14:21:02.396Z");
            var res = await dataClient.GetDataAsync(currDate, null);

            res.Date.Should().Be(currDate.ToUniversalTime());
        }
        
        [Fact]
        public async Task ShouldReturnCorrectDataSet()
        {
            var dataClient = new DataClient(BASE_URI, Client);
            var currDate = DateTime.Parse("2021-06-24T14:21:02.396Z");
            var res = await dataClient.GetDataAsync(currDate, null);

            var expected = new List<string>
            {
                "chicken",
                "dog"
            };
            res.DataSet.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task ShouldFail()
        {
            var dataClient = new DataClient(BASE_URI, Client);
            var currDate = DateTime.Parse("2021-06-24T14:21:02.396Z");

            var ex = await Assert.ThrowsAsync<ApiException<ValidationProblemDetails>>(
                () => dataClient.GetDataAsync(currDate, 0));
            
            ex.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            ex.Result.Errors.Keys.Should().Contain("MaxResultCount");
            ex.Result.Errors["MaxResultCount"].Should().BeEquivalentTo("Page must be a positive non zero integer");
        }
    }
}