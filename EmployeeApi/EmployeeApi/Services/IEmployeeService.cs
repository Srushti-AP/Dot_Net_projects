using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EmployeeApi.Models;


namespace EmployeeApi.Services
{
    public interface IEmployeeService
    {
         void AddEmployee(Employee employee);
    List<Employee> GetEmployees();
    void UpdateEmployee(int id, Employee employee);
    void DeleteEmployee(int id);
    }
}