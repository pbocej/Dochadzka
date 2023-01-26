using Doch.Models;
using Doch.Models.Exceptions;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Doch.Web.Code
{
    public class ApiClient : IApiClient
    {
        public string APIBaseUrl => _baseUrl;

        private readonly string _baseUrl;
        private readonly string _dochPath = "/api/doch";
        private readonly IConfiguration _configuration;

        public ApiClient(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration["APIBaseUrl"] ?? "NO_API_URL";
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

        public async Task<IEnumerable<Employee>> GetEmployeeList()
        {
            using HttpClient client = GetClient();
            HttpResponseMessage response = await client.GetAsync(_dochPath);
            if (response.IsSuccessStatusCode)
            {
                string textResponse = await response.Content.ReadAsStringAsync();
                if (textResponse != null)
                {
                    List<Employee>? list = JsonConvert.DeserializeObject<List<Employee>>(textResponse);
                    return list ?? new List<Employee>();
                }
            }
            throw new DataException($"{response.StatusCode}: {response.RequestMessage}");
        }

        public Task<int> CreateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
