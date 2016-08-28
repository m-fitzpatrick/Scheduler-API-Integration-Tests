using System;
using System.Net.Http;

namespace Scheduler.API.IntegrationTests.ApiClient
{
    public class ApiClientHelper
    {
        private readonly string _serviceHostStem;

        private readonly string _authenticationToken;

        public ApiClientHelper(string serviceHostStem, string authenticationToken)
        {
            _serviceHostStem = serviceHostStem;
            _authenticationToken = authenticationToken;
        }

        public TResult GetResult<TResult>(string route)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_serviceHostStem);
                httpClient.SetClientHeaders(_authenticationToken);

                return httpClient.GetAsync<TResult>(route).Result;
            }
        }

        public TResult PostResult<TPostData, TResult>(string route, TPostData postData)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_serviceHostStem);
                httpClient.SetClientHeaders(_authenticationToken);

                return httpClient.PostResultAsync<TPostData, TResult>(route, postData).Result;
            }
        }

        public void PostVoidResult<TPostData>(string route, TPostData postData)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_serviceHostStem);
                httpClient.SetClientHeaders(_authenticationToken);

                httpClient.PostVoidResultAsync(route, postData).Wait();
            }
        }

        public void Delete(string route)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_serviceHostStem);
                httpClient.SetClientHeaders(_authenticationToken);

                httpClient.Delete(route).Wait();
            }
        }
    }
}
