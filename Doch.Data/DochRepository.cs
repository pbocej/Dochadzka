using Doch.Models;

namespace Doch.Data
{
    public class DochRepository : IDochRepository
    {
        public IEnumerable<Employee> GetEmployees()
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Employee AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Employee UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Employee DeleteEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
