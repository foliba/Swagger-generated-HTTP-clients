using System.Net.Http;

namespace WebApplication.Test.Tests
{
    public abstract class BaseTest
    {
        protected const string BASE_URI = "https://localhost";
        protected readonly HttpClient Client;

        protected BaseTest()
        {
            var appFactory = new WebApplicationTestFactory();
            Client = appFactory.Server.CreateClient();
        }
    }
}