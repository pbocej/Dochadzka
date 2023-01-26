﻿using Doch.Models;

namespace Doch.Web.Code
{
    public interface IApiClient
    {
        string APIBaseUrl { get; }
        Task<int> CreateEmployee(Employee employee);
        Task DeleteEmployee(int id);
        Task<Employee> GetEmployee(int id);
        Task<IEnumerable<Employee>> GetEmployeeList();
        Task<int> UpdateEmployee(Employee employee);

        Task<IEnumerable<Position>> GetPositions();
    }
}
