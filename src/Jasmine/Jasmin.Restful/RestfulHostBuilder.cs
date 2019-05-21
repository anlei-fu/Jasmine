using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jasmine.Restful.Implement
{
    public class JasmineHostBuilder : IWebHostBuilder
    {
        public IWebHost Build()
        {
            throw new NotImplementedException();
        }

        public IWebHostBuilder ConfigureAppConfiguration(Action<WebHostBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            throw new NotImplementedException();
        }

        public IWebHostBuilder ConfigureServices(Action<IServiceCollection> configureServices)
        {
            throw new NotImplementedException();
        }

        public IWebHostBuilder ConfigureServices(Action<WebHostBuilderContext, IServiceCollection> configureServices)
        {
            throw new NotImplementedException();
        }

        public string GetSetting(string key)
        {
            throw new NotImplementedException();
        }

        public IWebHostBuilder UseSetting(string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}
