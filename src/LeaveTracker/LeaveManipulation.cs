using System;
using System.Collections.Generic;
using System.IO;

namespace LeaveTracker
{
    public class LeaveManipulation
    {
        Leave leave = new Leave();
        FileReadWrite file = new FileReadWrite();
        public void AddingLeave(int id)
        { 
            Employee emp = file.GetEmployee(id);
            Leave leave = null;
            Console.WriteLine("Enter Title For Leave :");
            string title = Console.ReadLine();
            Console.WriteLine("Enter Description for Leave :");
            string Description = Console.ReadLine();
             Console.WriteLine("Enter Start Date For Leave (dd/mm/yyyy) :");
            DateTime StartDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter End Date For Leave (dd/mm/yyyy) :");
            DateTime EndDate = DateTime.Parse(Console.ReadLine()) ;
            leave = CreateLeave(title,Description,StartDate, EndDate,emp);
            file.ReadDataFromFile(id, leave);
        }
        public Leave CreateLeave (string title, string description, DateTime startDate, DateTime endDate, Employee emp) {
            return new Leave(emp.GetEmpId(),emp.GetCreator(), (file.GetEmployee(emp.GetManagerId())).GetCreator(), title, description, startDate, endDate);
        }
        public void ListMyLeaves(int id)
        {
            if(leave.IsLeaveExits(id))
            {
                HashSet<Leave> TempLeaveList = new HashSet<Leave>();
                TempLeaveList = file.GetMyLeaves(id);    
                Employee emp = file.GetEmployee(id);
                Console.WriteLine($"Hello {emp.GetCreator()}");      
                leave.Display(TempLeaveList);
            }
            else
            Console.WriteLine($"There is No leave For id = {id} ");
        }
        public void UpdateLeaveStatus(int id)
        {
            int leaveId;
            ListMyLeaves(id);
            Console.WriteLine("Enter Leave Id : ");
            leaveId =Int32.Parse(Console.ReadLine());
            
            Console.WriteLine("Can You Enter the updated status of Leave (Approved | Pending | Rejected ):");
            string status = Console.ReadLine().ToUpper();
            Console.WriteLine($"Current Status of {leave.GetEmployeeName()} is {leave.GetStatus()}");
           
            if(file.UpdateStatus(leaveId,status))
            {
                file.UpdateDataFromFile();
                Console.WriteLine("Your Data is Updated Successfully...");
            }
            else
            Console.WriteLine("Your Data Updation is Failed... Please Enter correct Level Id");

            Console.WriteLine($"Updates Status of {leave.GetEmployeeName()} is {leave.GetStatus()}");

        }

        public void SearchLeaveByTitle()
        {
            Console.WriteLine("Please Enter Title of Leave");
            string title = Console.ReadLine();
            HashSet<Leave> TempLeaveList = new HashSet<Leave>();    
            TempLeaveList = file.LeaveByTitle(title);
            leave.Display(TempLeaveList);
        }

        public void SearchLeaveByStatus()
        {
            Console.WriteLine("Please Enter Status of Leave");
            string status = Console.ReadLine().ToUpper();
            HashSet<Leave> TempLeaveList = new HashSet<Leave>();
            TempLeaveList = file.LeaveByStatus(status);
            leave.Display(TempLeaveList);           
        }
    }
}