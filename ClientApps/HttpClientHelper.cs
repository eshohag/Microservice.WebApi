using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientApps
{
    public abstract class HttpClientHelper
    {
        private readonly IConfiguration _configuration;

        public HttpClientHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static async Task<T> PostAsync<T>(dynamic model, string url, string token = "", string fToken = "")
        {
            var httpClient = new HttpClient();

            try
            {
                if (!String.IsNullOrWhiteSpace(token))
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                if (!String.IsNullOrWhiteSpace(fToken))
                    httpClient.DefaultRequestHeaders.Add("F-TOKEN", $"{fToken}");

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var resposeResult = await response.Content.ReadAsStringAsync();

                    if (!String.IsNullOrWhiteSpace(resposeResult))
                        return JsonConvert.DeserializeObject<T>(resposeResult);
                }
                return default(T);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return default(T);
            }
        }

        public static async Task<T> GetAsync<T>(string url, string token = "", string fToken = "")
        {
            var httpClient = new HttpClient();
            try
            {
                if (!String.IsNullOrWhiteSpace(token))
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                if (!String.IsNullOrWhiteSpace(fToken))
                    httpClient.DefaultRequestHeaders.Add("F-TOKEN", $"{fToken}");

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var resposeResult = await response.Content.ReadAsStringAsync();

                    if (!String.IsNullOrWhiteSpace(resposeResult))
                        return JsonConvert.DeserializeObject<T>(resposeResult);
                }
                return default(T);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return default(T);
            }
        }
    }
}
