using Jasmine.Serialization;
using log4net;
using System;
using System.Net;
using System.Runtime.CompilerServices;

namespace Jasmine.HttpClient
{
    public class JasmineHttpClient
    {
        private ISerializer _serializer;
   
        private ILog _logger;
        private IRestfulServiceConfigProvider _serviceProvider;
        public JasmineHttpClient(ILog logger,ISerializer serializer)
        {
            _serializer=serializer;
        }
   
        /// <summary>
        /// no return data ,http visit
        /// </summary>
        /// <param name="serviceGroup"></param>
        /// <param name="service"></param>
        /// <param name="obj"></param>
        public void Upload(string serviceGroup,string service,object obj=null)
        {
            Exception error = null;
            var srv = _serviceProvider.GetServiceGroup(serviceGroup)?.GetService(service);

            if (srv == null)
                throw new NotSupportedException($"the service({service}) can not be found in service-group({serviceGroup})! ");

            for (int i = 0; i < srv.MaxRetryTime; i++)
            {
                var statItem = new ServiceStatItem();

                var request = createRequest(srv,obj);

                try
                {
                    request.GetResponse();
                    statItem.FinishTime = DateTime.Now;
                    statItem.Successed = false;
                    srv.Stat.Add(statItem);

                    return;
                }
                catch (Exception ex)
                {
                    error = ex;
                    _logger?.Error(ex);
                }

                statItem.FinishTime = DateTime.Now;
                statItem.Successed = false;
                srv.Stat.Add(statItem);
            }

            if (srv.Throwlable)
                throw error;

        }
        /// <summary>
        /// with return,http visit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceGroup"></param>
        /// <param name="service"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public T DownLoad<T>(string serviceGroup, string service, object obj=null)
        {
            Exception error = null;
            var srv = _serviceProvider.GetServiceGroup(serviceGroup)?.GetService(service);

            if (srv == null)
                throw new NotSupportedException($"the service({service}) can not be found in service-group({serviceGroup})! ");

            for (int i = 0; i < srv.MaxRetryTime; i++)
            {
                var statItem = new ServiceStatItem();
                var request =createRequest(srv,obj);

                try
                {
                    if (_serializer.TryDeserialize<T>(request.GetResponse().GetResponseStream(), out var result))
                    {
                        statItem.FinishTime = DateTime.Now;
                        statItem.Successed = false;
                        srv.Stat.Add(statItem);
                        return result;
                    }
                    else
                    {
                        throw new Exception("deserialize error happend!");
                    }
                }
                catch (Exception ex)
                {
                    error = ex;
                    _logger?.Error(ex);
                }

                statItem.FinishTime = DateTime.Now;
                statItem.Successed = false;
                srv.Stat.Add(statItem);
            }


            if (srv.Throwlable)
                throw error;
            else
                return default(T);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private HttpWebRequest createRequest(RestFulService service, object obj)
        {
            var isPost = false;
            var url = $"{service.Parent.Domain}/{service.Parent.Name}/{service.Name}";

            if (url.ToUpper() == "POST")
            {
                isPost = true;
            }
            else if (url.ToUpper() == "GET")
            {
                if (obj != null)
                    url += makeQueryString(obj);

                isPost = false;
            }
            else
            {
                throw new NotSupportedException($" http method : {service.Method} is not surported,get or post");
            }


            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Timeout = service.Timeout;

            if (isPost && obj != null && !_serializer.TrySerialize(obj, request.GetRequestStream()))
                throw new Exception("at serializing obj into request stream ,error occured!");

            return request;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string makeQueryString(object obj)
        {
            return null;
        }

     


    }
}
