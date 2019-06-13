using Jasmine.Common;
using Jasmine.Restful.Implement;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Threading.Tasks;

namespace Jasmine.Restful
{
    public class RestfulApplication
    {
        public RestfulApplication(int port, IDispatcher<HttpFilterContext> dispatcher)
        {
            _dispatcher = dispatcher;
            _port = port;
        }

        private IDispatcher<HttpFilterContext> _dispatcher;

        private int _port;

        private bool _portSeted;

        private IWebHost _webHost;

        public async Task StartAsync()
        {

            _webHost = WebHost.CreateDefaultBuilder()
                              .UseKestrel()
                              .ConfigureKestrel(
                                             option =>
                                             {
                                                 option.ConfigureEndpointDefaults(
                                                                kestrelOptions =>
                                                                {
                                                                    if (!_portSeted)
                                                                    {
                                                                        kestrelOptions.IPEndPoint = new IPEndPoint(IPAddress.Any, _port);

                                                                        _portSeted = true;
                                                                    }
                                                                });
                                             })
                              .ConfigureServices(
                                             services =>
                                            {
                                                services.AddSingleton<JasmineResfulMiddleware>();
                                            })
                              .Configure(
                                            app =>
                                            {
                                                JasmineResfulMiddleware.Dispatcher = _dispatcher;

                                                app.UseMiddleware<JasmineResfulMiddleware>();
                                            })
                              .Build();

            await _webHost.RunAsync();

        }
        public async Task StopAsync()
        {
            await _webHost.StopAsync();
        }


    }
}
