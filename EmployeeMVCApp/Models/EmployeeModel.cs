using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeWebApi.Models
{
    public class EmployeeModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Salary { get; set; }

    }
}