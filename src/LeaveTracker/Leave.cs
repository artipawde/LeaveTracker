using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace LeaveTracker
{
    public class Leave
    {
        private static int LastleaveId;
        private int leaveId;
        private int empId;
        private string employeeName;
        private string managerName;
        private string title;
        private string descripation;
        private DateTime startDate;
        private DateTime endDate;
        private StatusType status;
        private static HashSet<Leave> leaveList = new HashSet<Leave>();

        FileReadWrite file = new FileReadWrite();

        public Leave()
        {
            LastleaveId = file.LastLeaveId();
        }
        public Leave(int empId,string employeeName, string managerName, string title, string descripation,DateTime startDate, DateTime endDate)
        {
            this.leaveId = ++LastleaveId;
            this.empId = empId;
            this.employeeName = employeeName;
            this.managerName = managerName;
            this.title = title;
            this.descripation = descripation;
            this.startDate = startDate;
            this.endDate = endDate;
            this.status = StatusType.PENDING;
        }
        public Leave(int leaveId, int empId,string employeeName, string managerName, string title, string descripation,DateTime startDate, DateTime endDate, StatusType status)
        {
            this.leaveId = leaveId;
            this.empId = empId;
            this.employeeName = employeeName;
            this.managerName = managerName;
            this.title = title;
            this.descripation = descripation;
            this.startDate = startDate;
            this.endDate = endDate;
            this.status = status;
        }
        public int GetLeaveId()
        {
            return leaveId;
        }
        
        public int GetLastLeaveId()
        {
            return LastleaveId;
        }
        public void SetLastLeaveId(int leaveId)
        {
            LastleaveId = leaveId;
        }
        public void SetEmpId(int empId)
        {
            this.empId = empId;
        }
        public int GetEmpId()
        {
            return this.empId;
        }
        public void SetEmployeeName(string employeeName)
        {
            this.employeeName = employeeName;
        }
        public string GetEmployeeName()
        {
            return this.employeeName;
        }
        public void SetManagerName(string managerName)
        {
            this.managerName = managerName;
        }
        public string GetManagerName()
        {
            return this.managerName;
        }
        public void SetTitle(string title)
        {
            this.title = title;
        }
        public string GetTitle()
        {
            return this.title;
        }
        public void SetDescripation(string descripation)
        {
            this.descripation = descripation;
        }
        public string GetDescripation()
        {
            return this.descripation;
        }
        public void SetStartDate(DateTime startDate)
        {
            this.startDate = startDate;
        }
        public DateTime GetStartDate()
        {
            return this.startDate;
        }public void SetEndDate(DateTime endDate)
        {
            this.endDate = endDate;
        }
        public DateTime GetEndDate()
        {
            return this.endDate;
        }
        
        public void SetStatus(string status)
        {
            this.status = (StatusType)Enum.Parse(typeof(StatusType), status);
        }
        public StatusType GetStatus()
        {
            return this.status;
        }
        public void SetLeaveList(HashSet<Leave> leaves)
        {
            leaveList = leaves;
        }
        public HashSet<Leave> GetLeaveList()
        {
            return leaveList;
        }

        public bool IsLeaveExits(int id)
        {
            foreach(Leave leave in leaveList)
            {
                if(id == leave.GetEmpId())
                return true;
            }
            return false;
        }

        public void Display(HashSet<Leave> TempLeaveList)
        {     
            Console.WriteLine("-----------------------------------------------------");   
            Console.WriteLine($"Your Count of List of Leaves is : {TempLeaveList.Count}");
            Console.WriteLine("=======================================================");
            foreach (Leave leave in TempLeaveList)
            {
                Console.WriteLine($"Level Id : {leave.GetLeaveId()}");
                Console.WriteLine($"Employee Id : {leave.empId}");
                Console.WriteLine($"Title Of Leave : {leave.GetTitle()}");
                Console.WriteLine($"Descripation Of Leave : {leave.GetDescripation()}");
                Console.WriteLine($"Starting Date Of Leave : {leave.GetStartDate()}");
                Console.WriteLine($"Ending Date Of Leave : {leave.GetEndDate()}");
                Console.WriteLine($"Status Of Leave : {leave.GetStatus()}");
                Console.WriteLine("=======================================================");
            }    
        }
    }
}