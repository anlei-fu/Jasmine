using Jasmine.Restful.Implement;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Jasmine.Restful
{
    public class RestfulApplication
    {
        internal RestfulApplication()
        {
           
        }

        public event Action BeforeServerStart;
        public event Action AfterServerStart;
        public event Action BeforeServerStop;
        public event Action AfterServerStop;

        private readonly object _locker = new object();
        private bool _running = false;


        private IWebHost _server;

        public async Task<bool> StartAsync()
        {
            lock(_locker)
            {
                if (_running)
                    return false;

                BeforeServerStart?.Invoke();

                _running = true;
            }

            _server = WebHost.CreateDefaultBuilder()
                             .UseKestrel(kestrelOption=>
                             {

                                 kestrelOption.ListenAnyIP(RestfulApplicationGlobalConfig.ServerConfig.Port,listenConfig=>
                                 {
                                     listenConfig.NoDelay = RestfulApplicationGlobalConfig.ServerConfig.NoDelay;
                                     // = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
                                     
                                 });

                                 kestrelOption.ConfigureHttpsDefaults(httpConfig =>
                                 {
                                     if(RestfulApplicationGlobalConfig.ServerConfig.UseSsl)
                                     {
                                         httpConfig.HandshakeTimeout = RestfulApplicationGlobalConfig.ServerConfig.SslOption.HandshakeTimeout;
                                         httpConfig.ServerCertificate = new X509Certificate2(RestfulApplicationGlobalConfig.ServerConfig.SslOption.X509Certificate2Path);
                                         httpConfig.SslProtocols = RestfulApplicationGlobalConfig.ServerConfig.SslOption.SslProtocol;
                                         httpConfig.ClientCertificateMode = Microsoft.AspNetCore.Server.Kestrel.Https.ClientCertificateMode.NoCertificate;

                                     }
                                 });


                                 kestrelOption.Limits.KeepAliveTimeout = RestfulApplicationGlobalConfig.ServerConfig.KeepAliveTimeout;
                                 kestrelOption.Limits.MaxConcurrentConnections = RestfulApplicationGlobalConfig.ServerConfig.MaxCurrency;
                                 kestrelOption.Limits.MaxRequestBodySize = RestfulApplicationGlobalConfig.ServerConfig.MaxRequestBodySize;
                                 kestrelOption.Limits.MaxRequestBufferSize = RestfulApplicationGlobalConfig.ServerConfig.MaxRequestBufferSize;
                                 kestrelOption.Limits.MaxResponseBufferSize = RestfulApplicationGlobalConfig.ServerConfig.MaxResponseBufferSize;
                                 kestrelOption.Limits.MaxRequestLineSize = RestfulApplicationGlobalConfig.ServerConfig.MaxRequestLineSize;
                                 kestrelOption.Limits.MaxRequestHeadersTotalSize = RestfulApplicationGlobalConfig.ServerConfig.MaxRequestHeadersTotalSize;
                                 kestrelOption.Limits.MaxRequestHeaderCount = RestfulApplicationGlobalConfig.ServerConfig.MaxRequestHeaderCount;
                                 kestrelOption.Limits.RequestHeadersTimeout = RestfulApplicationGlobalConfig.ServerConfig.RequestHeadersTimeout;
                           
                             })
                             .ConfigureServices(provider=>
                             {
                                 provider.AddSingleton(typeof(ResfulMiddleware), RestfulApplicationBaseComponents.RestfulMiddleware);
                             })
                             .Configure(app=>
                             {
                                 app.UseMiddleware<ResfulMiddleware>();

                             })
                             .Build();

            await _server.RunAsync();

            AfterServerStart();

            return true;

        }
        public  async Task<bool> StopAsync()
        {
            lock (_locker)
            {
                if (_running)
                    return false;

                BeforeServerStop?.Invoke();

                _running = false;
            }

            await _server.StopAsync();

            AfterServerStop?.Invoke();

            return true;
        }


    }
}
