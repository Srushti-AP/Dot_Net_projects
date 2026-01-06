using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeApi.Models;

namespace EmployeeApi.Repository
{
    public interface IEmployeeRepository
    {
        void AddEmployee(Employee employee);
    List<Employee> GetEmployees();
    void UpdateEmployee(int id, Employee employee);
    void DeleteEmployee(int id);
    }
}