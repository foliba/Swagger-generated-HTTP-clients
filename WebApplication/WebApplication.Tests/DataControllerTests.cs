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
        private readonly DataClient _dataClient;

        public DataControllerTests()
        {
            _dataClient = new DataClient(BASE_URI, Client);
        }

        [Fact]
        public async Task ShouldReturnSameDateTime()
        {
            var currDate = DateTime.Parse("2021-06-24T14:21:02.396Z");
            var res = await _dataClient.GetDataAsync(currDate, null);

            res.Date.Should().Be(currDate.ToUniversalTime());
        }

        [Fact]
        public async Task ShouldReturnCorrectDataSet()
        {
            var currDate = DateTime.Parse("2021-06-24T14:21:02.396Z");
            var res = await _dataClient.GetDataAsync(currDate, null);

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
            var currDate = DateTime.Parse("2021-06-24T14:21:02.396Z");

            var ex = await Assert.ThrowsAsync<ApiException<ValidationProblemDetails>>(
                () => _dataClient.GetDataAsync(currDate, 0));

            ex.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);
            ex.Result.Errors.Keys.Should().Contain("MaxResultCount");
            ex.Result.Errors["MaxResultCount"].Should().BeEquivalentTo("Page must be a positive non zero integer");
        }

        [Fact]
        public async Task ShouldThrowErrorWhenUsingInvalidDateTime()
        {
            var invalidDate = DateTime.Parse("2021-06-24T14:21:02.396");
            var ex = await Assert.ThrowsAsync<ApiException<ValidationProblemDetails>>(
                         () => _dataClient.GetDataAsync(invalidDate, null));
            
            ex.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);
            ex.Result.Errors.Keys.Should().Contain("Date");
            ex.Result.Errors["Date"].Should().BeEquivalentTo("date must be valid RFC3339 formatted date string");
        }
    }
}