using LiteDB;
using LitedbTestProj.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LitedbTestProj
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var db = new LiteDatabase(@"C:\Users\Ashley Johansson\Documents\Intern\TestProj\LitedbTestProj\LitedbTestProj\TestDB\Test.db");
                // Get Employee collection if it exists, otherwise create a new one
                var Employees = db.GetCollection<EmployeeTestModel>("EmployeeTestModel");

                // Create a new Employee
                var EmployeeTestModel = new EmployeeTestModel
                {
                    EmployeeName = "Tapas Pal",
                    CompanyName = "TCS",
                    Department = "IT",
                    Skill = ".NET,C#,SQL,HTML",
                    PhoneNumber = "1234567890",
                    Address = "Kolkata,India"
                };

                // Create another Employee
                var Employee1 = new EmployeeTestModel
                {
                    EmployeeName = "XYZ ZYZ",
                    CompanyName = "TCS",
                    Department = "IT",
                    Skill = ".NET,C#,SQL,HTML",
                    PhoneNumber = "1234567899",
                    Address = "Kolkata,India"
                };

                //Insert Records
                Employees.Insert(EmployeeTestModel);
                Employees.Insert(Employee1);
                Console.WriteLine(EmployeeTestModel.EmployeeId);
                Console.WriteLine(Employee1.EmployeeId);

                //Update Record
                EmployeeTestModel.EmployeeName = "New Employee";
                Employees.Upsert(EmployeeTestModel);
                int eID = EmployeeTestModel.EmployeeId;

                //Find Record
                var collection = db.GetCollection<EmployeeTestModel>("EmployeeTestModel");
                var EmployeeFine = collection.FindById(EmployeeTestModel.EmployeeId);
                Console.WriteLine(EmployeeFine.EmployeeName + " " + EmployeeFine.EmployeeId);

                var results = collection.Find(x => x.Address.Contains("Kolkata"));

                Console.WriteLine(results.ElementAt(EmployeeFine.EmployeeId-1).EmployeeId +
                    " " + EmployeeFine.EmployeeName + " " + results.ElementAt(Employee1.EmployeeId-1).EmployeeId +
                    " " + Employee1.EmployeeName);

                //Create Index
                Employees.EnsureIndex("EmployeeId");
                Employees.Delete(EmployeeTestModel.EmployeeId);
                Employees.Delete(Employee1.EmployeeId);
            }
            catch (Exception ex)
            {

            }

        }
    }
}
