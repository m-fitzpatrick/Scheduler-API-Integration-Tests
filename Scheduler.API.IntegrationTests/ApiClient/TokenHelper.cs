using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Scheduler.API.IntegrationTests.Models;

namespace Scheduler.API.IntegrationTests.ApiClient
{
    public class TokenHelper
    {
        private readonly string _serviceHostStem;

        private const string _tokenRoute = "token";

        public TokenHelper(string serviceHostStem)
        {
            _serviceHostStem = serviceHostStem;
        }

        public async Task<string> GetToken(string userName, string password)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_serviceHostStem);
                FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
                                                                          {
                                                                              new KeyValuePair<string, string>("username", userName),
                                                                              new KeyValuePair<string, string>("password", password),
                                                                              new KeyValuePair<string, string>("grant_type", "password")
                                                                          });
                HttpResponseMessage response = await httpClient.PostAsync(_tokenRoute, content);
                response.EnsureSuccessStatusCode();

                string responseBody = response.Content.ReadAsStringAsync().Result;

                var accessToken = JsonConvert.DeserializeObject<AuthToken>(responseBody);

                return accessToken.access_token;
            }
        }
    }
}
