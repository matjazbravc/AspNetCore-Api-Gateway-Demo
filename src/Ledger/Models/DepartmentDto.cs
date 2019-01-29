using System.Collections.Generic;

namespace Department.Models
{
    public class DepartmentDto
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; }
		
        public ICollection<string> Employees { get; set; } = new HashSet<string>();
    }
}
