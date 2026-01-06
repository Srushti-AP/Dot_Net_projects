using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EmployeeApi.Data;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using EmployeeApi.Services;


namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
   {
      private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost]
    public IActionResult AddEmployee(Employee employee)
    {
        _employeeService.AddEmployee(employee);
        return Ok("Employee added successfully");
    }

    [HttpGet]
    public IActionResult GetEmployees()
    {
        var employees = _employeeService.GetEmployees();
        return Ok(employees);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateEmployee(int id, Employee employee)
    {
        _employeeService.UpdateEmployee(id, employee);
        return Ok("Employee updated successfully");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEmployee(int id)
    {
        _employeeService.DeleteEmployee(id);
        return Ok("Employee deleted successfully");
    }
}
}
