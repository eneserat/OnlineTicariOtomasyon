using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineTicariOtomasyon.Models.Classes;    
namespace OnlineTicariOtomasyon.Models.Classes
{
    public class DepartmentDetailsVM
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public List<string> EmployeeNames { get; set; }
        public List<EmployeeVM> Employees { get; set; }

    }
    public class EmployeeVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}