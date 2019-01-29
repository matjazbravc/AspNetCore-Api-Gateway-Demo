using System.Collections.Generic;
using System.Linq;
using Department.Models;
using Microsoft.AspNetCore.Mvc;

namespace Department.Controllers
{
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        private readonly List<DepartmentDto> _departments = new List<DepartmentDto>()
        {
            new DepartmentDto { DepartmentId = 1, Name = "HR" },
            new DepartmentDto { DepartmentId = 2, Name = "Admin" },
            new DepartmentDto { DepartmentId = 3, Name = "Development" }
        };

        [HttpGet]
        public List<DepartmentDto> All()
        {
            return _departments;
        }

        [HttpGet("getbyid/{id}")]
        public DepartmentDto GetById(int id)
        {
            return _departments.FirstOrDefault(t => t.DepartmentId == id);
        }
    }
}
