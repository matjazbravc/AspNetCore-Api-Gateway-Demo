using System;

namespace Employee.Models
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public int CompanyId { get; set; }
    }
}
