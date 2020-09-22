using System;
using System.Collections.Generic;
using System.IO;

namespace LeaveTracker
{
    public class Employee
    {
        private int empId;
        private static List<Employee> empList = new List<Employee>();
        private string creatorName;
        private int managerId;
        private string managerName;

        public Employee()
        {}
        public Employee(int empId, string creatorName, int managerId)
        {
            this.empId = empId;
            this.creatorName = creatorName;
            this.managerId = managerId;
        }

        public void SetEmpId(string no)
        {
            this.empId = Int32.Parse(no);
        }
        public void SetEmpList(List<Employee> list)
        {
            empList = list;
        }
        public List<Employee> GetEmpList()
        {
            return empList;
        }
        public int GetEmpId()
        {
            return this.empId;
        }
        public void SetCreator(string creatorName)
        {
            this.creatorName = creatorName;
        }
        public string GetCreator()
        {
            return this.creatorName;
        }
        public void SetMangerName(string managerName)
        {
            this.managerName = managerName;
        }
        public string GetMangerName()
        {
            return this.managerName;
        }
        public void SetManagerId(string no)
        {
            this.managerId = Int32.Parse(no);
        }
        public int GetManagerId()
        {
            return managerId;
        }


        public void Display()
        {
            Console.WriteLine($"EmpId : {empId}");
            Console.WriteLine($"Creator Name : {creatorName}");
            Console.WriteLine($"Manager Name : {managerName}");
            Console.WriteLine("==================================");
        }


        public int CheckIdIsValid()
        {       
           Console.WriteLine("Enter Employee Id");
           int empId = Int32.Parse(Console.ReadLine());
           Console.WriteLine("Enter Manager Id");
           int mgrId = Int32.Parse(Console.ReadLine());
         
            foreach(var emp in empList)
            {
                if(emp.GetEmpId() == empId && emp.GetManagerId() == mgrId)
                {
                    return empId;
                }
            }
            return 0;
        }        
    }
}