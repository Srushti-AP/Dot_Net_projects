using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EmployeeApi.Data;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/employee
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            // employee.CreatedDate = DateTime.Now;
            // employee.ModifiedDate = DateTime.Now;
            // _context.Employee.Add(employee);
            // _context.SaveChanges();
            _context.Database.ExecuteSqlRaw("exec AddEmployee @FirstName, @LastName,@Department,@Email", 
        new SqlParameter("@FirstName", employee.FirstName),
        new SqlParameter("@LastName", employee.LastName),
        new SqlParameter("@Department", employee.Department),
        new SqlParameter("@Email", employee.Email));

            return Ok("Employee added successfully");
        }

        // GET: api/employee
        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = _context.Employee.FromSqlRaw("exec GetEmployee").ToList();
            return Ok(employees);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, Employee emp)
       {
         _context.Database.ExecuteSqlRaw(
        "EXEC UpdateEmployee @Id, @FirstName, @LastName, @Department, @Email",
        new SqlParameter("@Id", id),
        new SqlParameter("@FirstName", emp.FirstName),
        new SqlParameter("@LastName", emp.LastName),
        new SqlParameter("@Department", emp.Department),
        new SqlParameter("@Email", emp.Email)
    );
    return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEmployee(int id)
        {
            _context.Database.ExecuteSqlRaw(
                "exec DeleteEmployee @Id",
                new SqlParameter("@Id",id)
            );
            return  Ok();;
        }

        
    }
}
