using AssignmentDemo.Entities.API.UserDetails;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AssignmentDemo.Provider.WebClient
{
    public class WebRequestHandler<T> : IWebRequestHandler<T>
    {
        public async Task<List<T>> GetDataByAll(string url)
        {
            List<T> result = null;
            try
            {
                var httpClient = HttpClientFactory.Create();
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);
                if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    var content = httpResponseMessage.Content;
                    var data = await content.ReadAsAsync<List<T>>();
                    result = data;
                }
            }
            catch
            {

            }
            return result;
        }

    }
}
