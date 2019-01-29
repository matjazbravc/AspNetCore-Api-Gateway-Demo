using System;
using System.Collections.Generic;
using System.Linq;
using Employee.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly List<EmployeeDto> _employees = new List<EmployeeDto>()
        {
            new EmployeeDto
            {
                EmployeeId = 1,
                FirstName = "John",
                LastName = "Whyne",
                BirthDate = new DateTime(1991, 8, 7),
                CompanyId = 1
            },
            new EmployeeDto
            {
                EmployeeId = 2,
                FirstName = "Mathias",
                LastName = "Gernold",
                BirthDate = new DateTime(1997, 10, 12),
                CompanyId = 1
            },
            new EmployeeDto
            {
                EmployeeId = 3,
                FirstName = "Julia",
                LastName = "Reynolds",
                BirthDate = new DateTime(1955, 12, 16),
                CompanyId = 1
            },
            new EmployeeDto
            {
                EmployeeId = 4,
                FirstName = "Alois",
                LastName = "Mock",
                BirthDate = new DateTime(1935, 2, 9),
                CompanyId = 2
            },
            new EmployeeDto
            {
                EmployeeId = 5,
                FirstName = "Gertraud",
                LastName = "Bochold",
                BirthDate = new DateTime(2001, 3, 4),
                CompanyId = 3
            }
        };

        [HttpGet]
        public List<EmployeeDto> All()
        {
            return _employees;
        }

        [HttpGet("getbycompanyid/{id}")]
        public List<EmployeeDto> GetByCompanyId(int id)
        {
            return _employees.Where(t => t.CompanyId == id).ToList();
        }
    }
}
