using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeApi.Repository;
using EmployeeApi.Models;

namespace EmployeeApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public void AddEmployee(Employee employee)
    {
        _employeeRepository.AddEmployee(employee);
    }

    public List<Employee> GetEmployees()
    {
        return _employeeRepository.GetEmployees();
    }

    public void UpdateEmployee(int id, Employee employee)
    {
        _employeeRepository.UpdateEmployee(id, employee);
    }

    public void DeleteEmployee(int id)
    {
        _employeeRepository.DeleteEmployee(id);
    }
}
    }
