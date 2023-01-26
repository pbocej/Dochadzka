using Doch.Models;
using Doch.Models.Exceptions;
using Doch.Models.FGIP;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;

namespace Doch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FGIPController : Controller
    {
        private readonly string _apiKey;
        private readonly string _apiUrl;
        private readonly string _baseUrl;

        public FGIPController(IConfiguration configuration)
        {
            var a = "https://api.ipbase.com/v2/info?apikey=kW22e3qqEWNH1xTnLe8qZAbQexQMg6ja2GjH143N&language=en&ip=92.60.51.21";
            _baseUrl = configuration.GetValue<string>("FreeGeoIP:BaseUrl");
            _apiUrl = configuration.GetValue<string>("FreeGeoIP:ApiUrl");
            _apiKey = configuration.GetValue<string>("FreeGeoIP:Key");
        }
        
        [HttpGet("country/{ip}")]
        public async Task<string> GetCountry(string ip)
        {
            var client = GetClient();
            HttpResponseMessage response = await client.GetAsync($"{_apiUrl}?apikey={_apiKey}&language=en&ip={ip}");
            if (response.IsSuccessStatusCode)
            {
                string textResponse = await response.Content.ReadAsStringAsync();
                if (textResponse != null)
                {
                    var obj = JsonConvert.DeserializeObject<CountryJson>(textResponse);
                    if (obj == null) 
                        return "NON";

                    return obj.Data.Location.Country.Alpha2;
                }
            }
            throw new DataException($"{response.StatusCode}: {response.RequestMessage}");
        }

        private HttpClient GetClient()
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri(_baseUrl)
            };
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
