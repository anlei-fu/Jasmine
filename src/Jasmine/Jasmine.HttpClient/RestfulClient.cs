using Jasmine.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Jasmine.HttpClient
{
    public class RestfulClient : IRestfulClient
    {

        public string BaseUrl { get; set; }
        public int Retry { get; set; } = -1;
        public int Timeout { get; set; }
        public bool Throwable { get; set; } = true;
        public Encoding Encoding { get; set; } = Encoding.UTF8;
        public IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>()
        {
            { "Content-Type","Application/Json"}
        };
        public CookieCollection Cookies { get; set; }
        public IServiceLookUp Provider { get; set; }
        public Action<HttpWebRequest> BeforeRequestInterceptor { get; set; }
        public Action<HttpWebResponse> AfterResponseInterceptor { get; set; }
        public Action<Exception> ErrorInterceptor { get; set; }
        public int TranferCallbackSize { get; set; }


        private HttpWebRequest createRequest(string apth,string method,int timeout,IDictionary<string,string>headers,CookieCollection cookies)
        {
            return null;
        }

        public void Delete(string path,
                           object body = null, 
                           int timeout = 10000, 
                           int retry = -1,
                           bool throwable = true,
                           Encoding encoding = null,
                           TransferCallback uploadProgress = null,
                           IDictionary<string, string> headers = null, 
                           CookieCollection cookies = null)
        {
            callNoReturn(path, "DELETE", body, timeout, retry,throwable, encoding, uploadProgress, headers, cookies);
        }

        private void writeIntoStream(Stream stream,object body,Encoding encoding,TransferCallback uploadCallback)
        {

        }
        private Task writeIntoStreamAsync(Stream stream, object body, Encoding encoding, TransferCallback uploadCallback)
        {

        }

        public T Delete<T>(string path,
                           object body = null, 
                           int timeout = 10000,
                           int retry = -1,
                           bool throwable = true,
                           Encoding encoding = null,
                           TransferCallback uploadProgress = null,
                           TransferCallback downloadProgress = null, 
                           IDictionary<string, string> headers = null, 
                           CookieCollection cookies = null)
        {
            return callReturn<T>(path,"DELETE", body, timeout, retry, throwable, encoding, uploadProgress, downloadProgress, headers, cookies);
        }

        private T callReturn<T>(string path,
                               string method,
                               object body,
                               int timeout,
                               int retry,
                               bool throwable,
                               Encoding encoding,
                               TransferCallback uploadProgress,
                               TransferCallback downloadProgress,
                               IDictionary<string, string> headers,
                               CookieCollection cookies)
        {
            var t = 0;

            Exception error = null;

            while (t++ < retry)
            {
                try
                {
                    var request = createRequest(path, method, timeout, headers, cookies);

                    var stream = request.GetRequestStream();

                    if (body != null)
                    {
                        writeIntoStream(stream, body, encoding, uploadProgress);
                    }

                    var respose = request.GetResponse();

                    var contentLengh = respose.ContentLength;
                   
                    return readStream<T>(contentLengh,respose.GetResponseStream(), encoding, downloadProgress);
                }
                catch (Exception ex)
                {
                    ErrorInterceptor?.Invoke(ex);
                    error = ex;
                }
            }

            if (throwable && error != null)
            {
                throw error;
            }
            else
            {
                return default(T);
            }
        }

        private  T readStream<T>(long contentLengh,Stream stream,Encoding encoding,TransferCallback downloadProgress)
        {
            long transferd = 0;

            var buffer = new byte[contentLengh];//10kb

            while (stream.Read(buffer,(int)transferd,TranferCallbackSize)>TranferCallbackSize)
            {
                transferd += TranferCallbackSize;

                downloadProgress?.Invoke(contentLengh, transferd);
            }

            downloadProgress?.Invoke(contentLengh, contentLengh);

            return JsonSerializer.Instance.Deserialize<T>(buffer, encoding);

        }

        private void callNoReturn(string path,
                                  string method,
                                  object body,
                                  int timeout,
                                  int retry,
                                  bool throwable,
                                  Encoding encoding,
                                  TransferCallback uploadProgress,
                                  IDictionary<string,string>headers,
                                  CookieCollection cookies )
        {

            var t = 0;

            Exception error = null;

            while (t++ < retry)
            {
                try
                {
                    var request = createRequest(path, method, timeout, headers, cookies);

                    var stream = request.GetRequestStream();

                    if (body != null)
                    {
                        writeIntoStream(stream, body, encoding, uploadProgress);
                    }

                    request.GetResponse();
                }
                catch (Exception ex)
                {
                    ErrorInterceptor?.Invoke(ex);
                    error = ex;
                }
            }

            if (throwable && error != null)
                throw error;
        }

        private async Task<T> callNoReturnAsync<T>(string path,
                                             string method,
                                             object body,
                                             int timeout,
                                             int retry,
                                             bool throwable,
                                             Encoding encoding,
                                             TransferCallback uploadProgress,
                                             TransferCallback downloadProgress,
                                             IDictionary<string, string> headers,
                                             CookieCollection cookies)
        {

            var t = 0;

            Exception error = null;

            while (t++ < retry)
            {
                try
                {
                    var request = createRequest(path, method, timeout, headers, cookies);

                    var stream = await request.GetRequestStreamAsync().ConfigureAwait(false);

                    if (body != null)
                    {
                        await writeIntoStreamAsync(stream, body, encoding, uploadProgress).ConfigureAwait(false);
                    }

                   var response= await request.GetResponseAsync().ConfigureAwait(false);

                    var contentLengh = response.ContentLength;

                    return readStream<T>(contentLengh, response.GetResponseStream(), encoding, downloadProgress);

                }
                catch (Exception ex)
                {
                    ErrorInterceptor?.Invoke(ex);
                    error = ex;
                }
            }

            if (throwable && error != null)
            {
                throw error;
            }
            else
            {
                return default(T);
            }
        }
        private async Task callNoReturnAsync(string path,
                                             string method,
                                             object body,
                                             int timeout,
                                             int retry,
                                             bool throwable,
                                             Encoding encoding,
                                             TransferCallback uploadProgress,
                                             IDictionary<string, string> headers,
                                             CookieCollection cookies)
        {

            var t = 0;

            Exception error = null;

            while (t++ < retry)
            {
                try
                {
                    var request = createRequest(path, method, timeout, headers, cookies);

                    var stream = await request.GetRequestStreamAsync().ConfigureAwait(false);

                    if (body != null)
                    {
                       await  writeIntoStreamAsync(stream, body, encoding, uploadProgress).ConfigureAwait(false);
                    }

                   await  request.GetResponseAsync().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    ErrorInterceptor?.Invoke(ex);
                    error = ex;
                }
            }

            if (throwable && error != null)
                throw error;
        }

        public async Task DeleteAsync(string path, 
                                object body = null, 
                                int timeout = 10000,
                                int retry = -1, 
                                bool throwable = true,
                                Encoding encoding = null,
                                TransferCallback uploadProgress = null,
                                TransferCallback downloadProgress = null, 
                                IDictionary<string, string> headers = null, 
                                CookieCollection cookies = null)
        {
            await callNoReturnAsync(path,"DELETE", body, timeout, retry, throwable, encoding, uploadProgress, headers, cookies);
        }

        public  Task<T> DeleteAsync<T>(string path,
                                      object body = null,
                                      int timeout = 10000, 
                                      int retry = -1, 
                                      bool throwable = true, 
                                      Encoding encoding = null,
                                      TransferCallback uploadProgress = null,
                                      TransferCallback downloadProgress = null, 
                                      IDictionary<string, string> headers = null,
                                      CookieCollection cookies = null)
        {
            return callNoReturnAsync<T>(path, 
                                       "DELETE",
                                       body, 
                                       timeout,
                                       retry, 
                                       throwable,
                                       encoding,
                                       uploadProgress,
                                       downloadProgress,
                                       headers,
                                       cookies);
        }

        public void Get(string path, 
                        int timeout = 10000, 
                        int retry = -1,
                        bool throwable = true,
                        Encoding encoding = null, 
                        TransferCallback uploadProgress = null,
                        TransferCallback downloadProgress = null, 
                        IDictionary<string, string> headers = null, 
                        CookieCollection cookies = null)
        {
            callNoReturn(path, "GET",null,timeout, retry, throwable, encoding, uploadProgress, headers, cookies);
        }

        public T Get<T>(string path,
                        int timeout = 10000,
                        int retry = -1,
                        bool throwable = true,
                        Encoding encoding = null,
                        TransferCallback uploadProgress = null,
                        TransferCallback downloadProgress = null, 
                        IDictionary<string, string> headers = null, 
                        CookieCollection cookies = null)
        {
            return callReturn<T>(path, "DELETE", null, timeout, retry, throwable, encoding, uploadProgress, downloadProgress, headers, cookies);
        }

        public Task GetAsync(string path, 
                            int timeout = 10000, 
                            int retry = -1,
                            bool throwable = true,
                            Encoding encoding = null, 
                            TransferCallback uploadProgress = null, 
                            TransferCallback downloadProgress = null, 
                            IDictionary<string, string> headers = null,
                            CookieCollection cookies = null)
        {
            throw new NotImplementedException();
        }

        public Task<Task> GetAsync<T>(string path, 
                                      int timeout = 10000, 
                                      int retry = -1, 
                                      bool throwable = true,
                                      Encoding encoding = null, 
                                      TransferCallback uploadProgress = null,
                                      TransferCallback downloadProgress = null, 
                                      IDictionary<string, string> headers = null,
                                      CookieCollection cookies = null)
        {
            throw new NotImplementedException();
        }

        public void Post(string path, 
                         object body, 
                         int timeout = 10000, 
                         int retry = -1, 
                         bool throwable = true, 
                         Encoding encoding = null,
                         TransferCallback uploadProgress = null,
                         TransferCallback downloadProgress = null,
                         IDictionary<string, string> headers = null,
                         CookieCollection cookies = null)
        {
            callNoReturn(path, "POST", body, timeout, retry, throwable, encoding, uploadProgress, headers, cookies);
        }

        public T Post<T>(string path, 
                         object body, 
                         int timeout = 10000, 
                         int retry = -1,
                         bool throwable = true,
                         Encoding encoding = null, 
                         TransferCallback uploadProgress = null, 
                         TransferCallback downloadProgress = null,
                         IDictionary<string, string> headers = null, 
                         CookieCollection cookies = null)
        {
            return callReturn<T>(path, "POST", body, timeout, retry, throwable, encoding, uploadProgress, downloadProgress, headers, cookies);
        }

        public Task PostAsync(string path,
                              object body,
                              int timeout = 10000,
                              int retry = -1,
                              bool throwable = true, 
                              Encoding encoding = null, 
                              TransferCallback uploadProgress = null,
                              TransferCallback downloadProgress = null, 
                              IDictionary<string, string> headers = null,
                              CookieCollection cookies = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> PostAsync<T>(string path, 
                                    object body,
                                    int timeout = 10000, 
                                    int retry = -1,
                                    bool throwable = true, 
                                    Encoding encoding = null, 
                                    TransferCallback uploadProgress = null, 
                                    TransferCallback downloadProgress = null,
                                    IDictionary<string, string> headers = null, 
                                    CookieCollection cookies = null)
        {
            throw new NotImplementedException();
        }

        public void Put(string path, 
                        object body = null,
                        int timeout = 10000,
                        int retry = -1, 
                        bool throwable = true, 
                        Encoding encoding = null,
                        TransferCallback uploadProgress = null,
                        TransferCallback downloadProgress = null,
                        IDictionary<string, string> headers = null, 
                        CookieCollection cookies = null)
        {
            callNoReturn(path, "PUT", body, timeout, retry, throwable, encoding, uploadProgress, headers, cookies);
        }

        public T Put<T>(string path,
                        object body = null,
                        int timeout = 10000, 
                        int retry = -1, 
                        bool throwable = true,
                        Encoding encoding = null,
                        TransferCallback uploadProgress = null,
                        TransferCallback downloadProgress = null,
                        IDictionary<string, string> headers = null, 
                        CookieCollection cookies = null)
        {
            return callReturn<T>(path, "PUT", body, timeout, retry, throwable, encoding, uploadProgress, downloadProgress, headers, cookies);
        }

        public Task PutAsync(string path,
                             object body = null, 
                             int timeout = 10000, 
                             int retry = -1, 
                             bool throwable = true,
                             Encoding encoding = null, 
                             TransferCallback uploadProgress = null,
                             TransferCallback downloadProgress = null,
                             IDictionary<string, string> headers = null,
                             CookieCollection cookies = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> PutAsync<T>(string path,
                                   object body = null,
                                   int timeout = 10000, 
                                   int retry = -1, 
                                   bool throwable = true,
                                   Encoding encoding = null,
                                   TransferCallback uploadProgress = null,
                                   TransferCallback downloadProgress = null, 
                                   IDictionary<string, string> headers = null, 
                                   CookieCollection cookies = null)
        {
            throw new NotImplementedException();
        }
    }
}
