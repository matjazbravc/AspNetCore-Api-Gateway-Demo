using System;
using System.Collections.Generic;

namespace Company.Models
{
    public class CompanyDto
    {
        public int CompanyId { get; set; }

        public string Name { get; set; }

        public ICollection<string> Employees { get; set; } = new HashSet<string>();
    }
}
