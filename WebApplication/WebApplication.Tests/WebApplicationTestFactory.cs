using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication.Test
{
    public class WebApplicationTestFactory: WebApplicationFactory<Startup>, IDisposable
    {
        public new void Dispose()
        {
            Dispose(true);
        }
        
        public new void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            builder.UseConfiguration(new ConfigurationBuilder().Build());

            builder.ConfigureServices(services =>
            {
                services.Configure<TestServer>(options => { options.AllowSynchronousIO = true; });
            });
        }
    }
}