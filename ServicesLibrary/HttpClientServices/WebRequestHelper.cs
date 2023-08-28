using Microsoft.IdentityModel.Abstractions;
using NuGet.Protocol.Plugins;
using ServicesLibrary.LogServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServicesLibrary.HttpClientServices
{
    public class WebRequestHelper : IWebRequestHelper
    {
        private readonly ILogService logService;

        public WebRequestHelper(ILogService logService)
        {
            this.logService = logService;
        }

        public async Task<object> GetRequestAsync(string url, Dictionary<string, string> headers)
        {
           using var client = new HttpClient();
            
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }

                try
                {
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var result = await response.Content.ReadFromJsonAsync<object>();
                    return result;
                }
                catch (HttpRequestException ex)
                {
                    logService.LogError("HTTP request failed", ex);

                    throw;
                }
                catch (JsonException ex)
                {
                    logService.LogError("JSON deserialization failed", ex);
                    throw;
                }
            
        }

    }
}
