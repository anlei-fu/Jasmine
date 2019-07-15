﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Jasmine.HttpClient
{

    public delegate void TransferCallback(long total, long trasferd);

  public  interface IRestfulClient
    {
        void Get(string path, 
                int timeout = 10 * 1000, 
                int retry=-1,
                bool throwable=true,
                Encoding encoing = null,
                TransferCallback uploadProgress = null,
                TransferCallback downloadProgress = null,
                IDictionary<string, string> headers = null,
                CookieCollection cookies = null);
        T Get<T>(string path,
                 int timeout = 10 * 1000,
                 int retry = -1, 
                 bool throwable = true,
                 Encoding encoing = null,
                 TransferCallback uploadProgress = null,
                 TransferCallback downloadProgress = null,
                 IDictionary<string, string> headers = null,
                 CookieCollection cookies = null);
        Task GetAsync(string path, 
                      int timeout = 10 * 1000, 
                      int retry = -1,
                      bool throwable = true,
                      Encoding encoing = null,
                      TransferCallback uploadProgress=null,
                      TransferCallback downloadProgress=null,
                      IDictionary<string, string> headers = null,
                      CookieCollection cookies = null);
        Task<Task> GetAsync<T>(string path,
                               int timeout = 10 * 1000,
                               int retry = -1, 
                               bool throwable = true,
                               Encoding encoing = null,
                               TransferCallback uploadProgress = null,
                               TransferCallback downloadProgress = null,
                               IDictionary<string, string> headers = null,
                               CookieCollection cookies = null);
        void Post(string path, 
                   object body, 
                   int timeout = 10 * 1000,
                   int retry = -1, 
                   bool throwable = true, 
                   Encoding encoing = null,
                   TransferCallback uploadProgress = null,
                   TransferCallback downloadProgress = null,
                   IDictionary<string, string> headers = null,
                   CookieCollection cookies = null);
        Task PostAsync(string path, 
                       object body, 
                       int timeout = 10 * 1000,
                       int retry = -1,
                       bool throwable = true,
                       Encoding encoing = null,
                       TransferCallback uploadProgress = null,
                       TransferCallback downloadProgress = null, 
                       IDictionary<string, string> headers = null, 
                       CookieCollection cookies = null);
        T Post<T>(string path, 
                  object body,
                  int timeout = 10 * 1000,
                  int retry = -1,
                  bool throwable = true,
                  Encoding encoing = null,
                  TransferCallback uploadProgress = null,
                  TransferCallback downloadProgress = null,
                  IDictionary<string, string> headers = null,
                  CookieCollection cookies = null);
        Task<T> PostAsync<T>(string path,
                             object body,
                             int timeout = 10 * 1000,
                             int retry = -1, 
                             bool throwable = true, 
                             Encoding encoing = null,
                             TransferCallback uploadProgress = null,
                             TransferCallback downloadProgress = null,
                             IDictionary<string, string> headers = null,
                             CookieCollection cookies = null);
        void Delete(string path,
                    object body = null,
                    int timeout = 10 * 1000,
                    int retry = -1, 
                    bool throwable = true,
                    Encoding encoing = null,
                    TransferCallback uploadProgress = null,
                    TransferCallback downloadProgress = null,
                    IDictionary<string, string> headers = null, 
                    CookieCollection cookies = null);
        Task DeleteAsync(string path,
                         object body = null, 
                         int timeout = 10 * 1000,
                         int retry = -1,
                         bool throwable = true,
                         Encoding encoing = null,
                         TransferCallback uploadProgress = null,
                         TransferCallback downloadProgress = null,
                         IDictionary<string, string> headers = null,
                         CookieCollection cookies = null);
         T Delete<T>(string path,
                     object body = null,
                     int timeout = 10 * 1000, 
                     int retry = -1,
                     bool throwable = true, 
                     Encoding encoing = null,
                     TransferCallback uploadProgress = null,
                     TransferCallback downloadProgress = null,
                     IDictionary<string, string> headers = null, CookieCollection cookies = null);
        Task<T> DeleteAsync<T>(string path,
                               object body = null,
                               int timeout = 10 * 1000,
                               int retry = -1,
                               bool throwable = true,
                               Encoding encoing = null,
                               TransferCallback uploadProgress = null,
                               TransferCallback downloadProgress = null,
                               IDictionary<string, string> headers = null,
                               CookieCollection cookies = null);
        void Put(string path, 
                 object body = null, 
                 int timeout = 10 * 1000,
                 int retry = -1,
                 bool throwable = true,
                 Encoding encoing = null,
                 TransferCallback uploadProgress = null,
                 TransferCallback downloadProgress = null,
                 IDictionary<string, string> headers = null, 
                 CookieCollection cookies = null);
        Task PutAsync(string path,
                 object body = null,
                 int timeout = 10 * 1000,
                 int retry = -1, 
                 bool throwable = true,
                 Encoding encoing = null,
                 TransferCallback uploadProgress = null,
                 TransferCallback downloadProgress = null,
                 IDictionary<string, string> headers = null, 
                 CookieCollection cookies = null);
         T Put<T>(string path,
                  object body = null,
                  int timeout = 10 * 1000,
                  int retry = -1,
                  bool throwable = true,
                  Encoding encoing = null,
                  TransferCallback uploadProgress = null,
                  TransferCallback downloadProgress = null,
                  IDictionary<string, string> headers = null,
                  CookieCollection cookies = null);
        Task<T> PutAsync<T>(string path,
                            object body = null,
                            int timeout = 10 * 1000, 
                            int retry = -1, 
                            bool throwable = true,
                            Encoding encoing = null,
                            TransferCallback uploadProgress = null,
                            TransferCallback downloadProgress = null,
                            IDictionary<string, string> headers = null,
                            CookieCollection cookies = null);
    }
}
