using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace LitedbTestProj.Models
{
    public class EmployeeTestModel
    {
        [BsonId]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string CompanyName { get; set; }
        public string Department { get; set; }
        public string Skill { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }

}
