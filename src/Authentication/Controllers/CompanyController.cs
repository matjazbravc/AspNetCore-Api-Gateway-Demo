using System.Collections.Generic;
using System.Linq;
using Company.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.Controllers
{
    [Route("api/[controller]")]
    public class CompanyController : Controller
    {
        private readonly List<CompanyDto> _companies = new List<CompanyDto>()
        {
            new CompanyDto()
            {
                CompanyId = 1,
                Name = "Company One"
            },
            new CompanyDto
            {
                CompanyId = 2,
                Name = "Company Two"
            },
            new CompanyDto
            {
                CompanyId = 3,
                Name = "Company Three"
            }
        };

        [HttpGet]
        public List<CompanyDto> All()
        {
            return _companies;
        }

        [HttpGet("getbyid/{id}")]
        public CompanyDto GetById(int id)
        {
            return _companies.FirstOrDefault(u => u.CompanyId == id);
        }
    }
}
