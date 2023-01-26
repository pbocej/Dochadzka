using Doch.Models;

namespace Doch.Data
{
    public interface IDochRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int employeeId);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<bool> DeleteEmployee(int employeeId);

        Task<IEnumerable<Position>> GetPositions();
    }
}
