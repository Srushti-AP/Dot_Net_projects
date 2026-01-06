using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using EmployeeApi.Models;
using EmployeeApi.Data;
using Microsoft.AspNetCore.Mvc;


namespace EmployeeApi.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }
    public void AddEmployee(Employee employee)
    {
        _context.Database.ExecuteSqlRaw(
            "exec AddEmployee @FirstName, @LastName, @Department, @Email",
            new SqlParameter("@FirstName", employee.FirstName),
            new SqlParameter("@LastName", employee.LastName),
            new SqlParameter("@Department", employee.Department),
            new SqlParameter("@Email", employee.Email)
        );
    }

    public List<Employee> GetEmployees()
    {
        return _context.Employee
                       .FromSqlRaw("exec GetEmployee")
                       .ToList();
    }

    public void UpdateEmployee(int id, Employee employee)
    {
        _context.Database.ExecuteSqlRaw(
            "exec UpdateEmployee @Id, @FirstName, @LastName, @Department, @Email",
            new SqlParameter("@Id", id),
            new SqlParameter("@FirstName", employee.FirstName),
            new SqlParameter("@LastName", employee.LastName),
            new SqlParameter("@Department", employee.Department),
            new SqlParameter("@Email", employee.Email)
        );
    }
    public void DeleteEmployee(int id)
    {
        _context.Database.ExecuteSqlRaw(
            "exec DeleteEmployee @Id",
            new SqlParameter("@Id", id)
        );
    }

    }
}