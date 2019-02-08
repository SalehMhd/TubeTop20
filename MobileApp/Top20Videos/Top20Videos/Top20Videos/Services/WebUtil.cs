using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using ModernHttpClient;
using Newtonsoft.Json;

namespace Top20Videos.Services
{
    public static class WebUtil
    {

        public static async Task<string> GetDataWithPostRequest<T>(string url, T requestModel)
        {
            string response = null;
            try
            {
                var httpClient = new HttpClient(new NativeMessageHandler());
                httpClient.Timeout = TimeSpan.FromMinutes(2);

                HttpResponseMessage data = await httpClient.GetAsync(url);

                if (data.StatusCode == HttpStatusCode.OK)
                {
                    response = await data.Content.ReadAsStringAsync();
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return null;
            }

            return response;
        }

        public static async Task<T> GetAsync<T>(string url)
        {
            var uri = new Uri(string.Format(url, string.Empty));
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("user", "Top20Video");
                client.DefaultRequestHeaders.Add("token", "pwSe12ojKfaA2w54ipFeP3SWwe9sd0N8m");
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(content);
                    return result;
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return default(T);
        }

    }
}
