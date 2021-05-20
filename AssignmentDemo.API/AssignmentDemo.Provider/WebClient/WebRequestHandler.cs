using AssignmentDemo.Entities.API.UserDetails;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AssignmentDemo.Provider.WebClient
{
    public class WebRequestHandler<T> : IWebRequestHandler<T>
    {

        private readonly IHttpClientFactory _clientFactory;
        public WebRequestHandler(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<T>> GetDataByAll(string url)
        {        
            var client = _clientFactory.CreateClient("AssignmentDemo");
            var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<List<T>>(); 

            return result;
        }
  

    }
}
