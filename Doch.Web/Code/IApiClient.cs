using Doch.Models;

namespace Doch.Web.Code
{
    public interface IApiClient
    {
        string APIBaseUrl { get; }
        Task<int> CreateEmployee(Employee employee);
        Task<bool> DeleteEmployee(int id);
        Task<Employee> GetEmployee(int id);
        Task<IEnumerable<Employee>> GetEmployeeList();
        Task<int> UpdateEmployee(Employee employee);

        Task<IEnumerable<Position>> GetPositions();
        Task<string> GetCoutryByIP(string ip);
    }
}
