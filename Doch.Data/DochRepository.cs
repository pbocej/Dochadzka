using Doch.Data.Exceptions;
using Doch.Models;
using Microsoft.EntityFrameworkCore;

namespace Doch.Data
{
    public class DochRepository : IDochRepository
    {
        private readonly DochContext _context;

        public DochRepository(DochContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employees
                .Include(i => i.Position)
                .ToListAsync();
        }

        public async Task<Employee> GetEmployee(int employeeId)
        {
            var emp = await _context.Employees
                .Include(i => i.Position)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            return emp ?? throw new DataNotFoundException("Employee not found.");
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));
            await _context.Employees.AddAsync(employee);
            var ret = await _context.SaveChangesAsync();
            if (ret == 0)
                throw new DataException("Error creating employee");
            return await _context.Employees
                .Include(i => i.Position)
                .FirstOrDefaultAsync(e => e.Name== employee.Name && e.SurName == employee.SurName) 
                ?? throw new DataException("Error creating employee");
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));
            _context.Employees.Update(employee);
            var ret = await _context.SaveChangesAsync();
            if (ret == 0)
                throw new DataException("Error updating employee");
            return employee;
        }

        public async Task<bool> DeleteEmployee(int employeeId)
        {
            var emp = await GetEmployee(employeeId);
            if (emp == null) throw new DataNotFoundException();
            _context.Employees.Remove(emp);
            var ret = await _context.SaveChangesAsync();
            if (ret == 0) throw new DataException("Error deleting employee");
            return true;
        }

        public async Task<IEnumerable<Position>> GetPositions()
        {
            return await _context.Positions.ToListAsync();
        }

    }
}
