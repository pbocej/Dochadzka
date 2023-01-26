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
        private readonly string _fgipPath = "/api/FGIP/country";
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

        public async Task<int> CreateEmployee(Employee employee)
        {
            using HttpClient client = GetClient();
            HttpResponseMessage response = await client.PostAsync(_dochPath, JsonContent.Create(employee, new MediaTypeHeaderValue("application/json")));
            string textResponse = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                case System.Net.HttpStatusCode.Created:
                    if (textResponse != null)
                    {
                        Employee? retEmployee = JsonConvert.DeserializeObject<Employee>(textResponse);
                        if (retEmployee != null)
                        {
                            return retEmployee.EmployeeId;
                        }
                    }
                    break;
                case System.Net.HttpStatusCode.NotFound:
                    throw new DataNotFoundException(textResponse);
                case System.Net.HttpStatusCode.Conflict:
                    throw new DataConflictException(textResponse);
                case System.Net.HttpStatusCode.BadRequest:
                    throw new DataException(textResponse);
            }
            throw new DataException($"{response.StatusCode}: {response.RequestMessage}");
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

        public async Task<IEnumerable<Position>> GetPositions()
        {
            using HttpClient client = GetClient();
            HttpResponseMessage response = await client.GetAsync($"{_dochPath}/positions");
            if (response.IsSuccessStatusCode)
            {
                string textResponse = await response.Content.ReadAsStringAsync();
                if (textResponse != null)
                {
                    List<Position>? list = JsonConvert.DeserializeObject<List<Position>>(textResponse);
                    return list ?? new List<Position>();
                }
            }
            throw new DataException($"{response.StatusCode}: {response.RequestMessage}");
        }

        public async Task<string> GetCoutryByIP(string ip)
        {
            using HttpClient client = GetClient();
            HttpResponseMessage response = await client.GetAsync($"{_fgipPath}/{ip}");
            if (response.IsSuccessStatusCode)
            {
                string textResponse = await response.Content.ReadAsStringAsync();
                if (textResponse != null)
                {
                    return textResponse.Replace("\"", "");
                }
            }
            throw new DataException($"{response.StatusCode}: {response.RequestMessage}");
        }
    }
}
