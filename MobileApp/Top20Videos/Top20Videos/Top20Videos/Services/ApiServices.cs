using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using NodaTime;

namespace Top20Videos.Services
{
    public class ApiServices
    {
        public class ServerApiException : Exception
        {
            public int ErrorCode { get; private set; }

            public ServerApiException() { }
            public ServerApiException(string message) : base(message) { }
            public ServerApiException(string message, Exception inner) : base(message, inner) { }
            public ServerApiException(string message, int errorCode) : base(message)
            {
                ErrorCode = errorCode;
            }
        }

        private static ApiServices instance;

        public static ApiServices Instance => instance ?? (instance = new ApiServices());

        public ApiServices() { }


        public async Task<TResponseModel> GetDataFromServerTask<TRequestModel, TResponseModel>(TRequestModel model, string url) where TRequestModel : class where TResponseModel : class, new()
        {
            var response = await WebUtil.GetDataWithPostRequest(url, model);
            TResponseModel result = default(TResponseModel);
            if (!string.IsNullOrEmpty(response))
            {
                try
                {
                    result = JsonConvert.DeserializeObject<TResponseModel>(response);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }
            return result;
        }

    }
}
