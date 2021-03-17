using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace WpfTest.UI
{
    public sealed class SingletonHttpClient : HttpClient
    {
        private static SingletonHttpClient _httpClient = null;
        private static readonly object padlock = new object();

        SingletonHttpClient()
        {
        }

        public static SingletonHttpClient HttpClient
        {
            get
            {
                lock (padlock)
                {
                    if (_httpClient == null)
                    {
                        _httpClient = new SingletonHttpClient();
                        _httpClient.BaseAddress = new Uri("http://localhost:1099/");
                        _httpClient.DefaultRequestHeaders.Accept.Clear();
                        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    }
                    return _httpClient;
                }
            }
        }
    }
}
