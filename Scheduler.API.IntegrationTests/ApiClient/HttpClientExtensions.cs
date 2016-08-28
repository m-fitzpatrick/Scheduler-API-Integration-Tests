using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Scheduler.API.IntegrationTests.ApiClient
{
    public static class HttpClientExtensions
    {
        public static void SetClientHeaders(this HttpClient httpClient, string authenticatonToken)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticatonToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<TResult> GetAsync<TResult>(this HttpClient httpClient, string requestUri)
        {
            HttpResponseMessage response = await httpClient.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(responseBody);
        }

        public static async Task<TResult> PostResultAsync<TPostData, TResult>(this HttpClient httpClient, string requestUri, TPostData postData)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(requestUri, postData);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(responseBody);
        }

        public static async Task PostVoidResultAsync<TPostData>(this HttpClient httpClient, string requestUri, TPostData postData)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(requestUri, postData);
            response.EnsureSuccessStatusCode();
        }

        public static async Task Delete(this HttpClient httpClient, string requestUri)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();
        }
    }
}
