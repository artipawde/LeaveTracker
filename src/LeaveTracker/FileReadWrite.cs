using System;
using System.IO;
using System.Collections.Generic;


namespace LeaveTracker
{
    public class FileReadWrite
    {     
        private string sourcePath = @"D:\LogFiles\employees.csv";
        private string destinationPath = @"E:\CsvFiles\leaves.csv";
        public void CreateFile()
        {
            if(!File.Exists(this.destinationPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(this.destinationPath));
            }
        }
        public void AddDataToList()
        {         
            AddingEmployeeDataToList();
            AddingLeaveDataToList();
        }
        public void ReadDataFromFile(int empId, Leave leave)
        {
            Employee emp = GetEmployee(empId);
            WriteDataToTheFile(leave.GetLeaveId() ,emp.GetEmpId(), emp.GetCreator(), leave.GetManagerName(), leave.GetTitle(), leave.GetDescripation(),leave.GetStartDate(),leave.GetEndDate(),leave.GetStatus());
        }
        public void UpdateDataFromFile()
        {
            File.Delete(destinationPath);
            HashSet<Leave> leaveList = new HashSet<Leave>();
            foreach(var leaves in leaveList)
            {
                WriteDataToTheFile(leaves.GetLeaveId() ,leaves.GetEmpId(), leaves.GetEmployeeName(), leaves.GetManagerName(), leaves.GetTitle(), leaves.GetDescripation(),leaves.GetStartDate(),leaves.GetEndDate(),leaves.GetStatus());           
            }
        }
        public void WriteDataToTheFile(int levelId, int empId, string creatorName, string managerName, string title,string description, DateTime startDate , DateTime endDate, StatusType status)
        {
            try
            {
                using(System.IO.StreamWriter file = new System.IO.StreamWriter(destinationPath, true))
                {                                                                                                                                                                                               
                    if(new FileInfo (destinationPath).Length == 0)
                    {
                        string header = "Id  | EmpId|  Creater Name | Manager Name  |    Title       |      Descripation      |   StartDate   |   EndDate  | Status\n";
                        file.WriteLine(header);
                    }
                    file.WriteLine(levelId + "  |" + empId + " | " + creatorName + "  | " + managerName + "     |" + title+ "| " + description + " | " + startDate + " | " + endDate + " |" + status);  
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public Employee GetEmployee(int empId)
        {
            List<Employee> list=GetAllEmployees();
            foreach (Employee emp in list)
            {
                if(emp.GetEmpId() == empId)
                    return emp;
            }
            return null;
        }
        public List<Employee> GetAllEmployees () {
            string[] lines = File.ReadAllLines (this.sourcePath);
            List<Employee> empList = new List<Employee>();
            for (int i = 1; i < lines.Length; i++) 
            {
                empList.Add (GetEmployee (lines[i]));
            }
            return empList;
        }
        public void AddingEmployeeDataToList()
        {
            Employee emp = new Employee();    
            List<Employee> list = GetAllEmployees();
            emp.SetEmpList(list);
        }
        public void AddingLeaveDataToList()
        {
            Leave leave = new Leave();
            HashSet<Leave> list = GetAllLeaves();
            leave.SetLeaveList(list);
        }
        public static Employee GetEmployee (string line) {
            string[] empData = line.Split (',',StringSplitOptions.RemoveEmptyEntries);
            try {
                return new Employee (Int32.Parse (empData[0]), empData[1], Int32.Parse(empData[2]));
            } catch (IndexOutOfRangeException) {
                return new Employee (Int32.Parse (empData[0]), empData[1], 0);
            }
        }
        public Leave GetLeave(int leaveId)
        {
            HashSet<Leave> list=GetAllLeaves();
            foreach (Leave leave in list)
            {
                if(leave.GetLeaveId() == leaveId)
                    return leave;
            }
            return null;
        }
        public int LastLeaveId()
        {
            string[] lines = File.ReadAllLines (this.destinationPath);
            if(lines.Length > 2)
            {
                return lines.Length - 2 ;       //one is Header and second one is blank line
            }
            return 0;
        }
        public HashSet<Leave> GetAllLeaves () {
            string[] lines = File.ReadAllLines (this.destinationPath);
            HashSet<Leave> leaveList = new HashSet<Leave>();
            for (int i = 2; i < lines.Length; i++) 
            {
                leaveList.Add (GetLeave (lines[i]));
            }
            return leaveList;
        }
        public HashSet<Leave> GetMyLeaves (int id) 
        {
            string[] lines = File.ReadAllLines (this.destinationPath);
            HashSet<Leave> tempList = new HashSet<Leave>(new LeaveIdCompare());
            for (int i = 2; i < lines.Length; i++) 
            {
                string[] leaveData = lines[i].Split ('|',StringSplitOptions.RemoveEmptyEntries);
                if(Int32.Parse(leaveData[1]) == id)
                {
                    tempList.Add(new Leave (Int32.Parse(leaveData[0]),Int32.Parse (leaveData[1]), leaveData[2], leaveData[3], leaveData[4], leaveData[5],DateTime.Parse(leaveData[6]),DateTime.Parse(leaveData[7]), (StatusType)Enum.Parse(typeof(StatusType),leaveData[8])));
                }
            }
            return tempList;
        } 
        public static Leave GetLeave (string line) 
        {
            string[] leaveData = line.Split ('|',StringSplitOptions.RemoveEmptyEntries);
            try{
            return new Leave (Int32.Parse (leaveData[0]) ,Int32.Parse (leaveData[1]), leaveData[2], leaveData[3], leaveData[4], leaveData[5],DateTime.Parse(leaveData[6]),DateTime.Parse(leaveData[7]), (StatusType)Enum.Parse(typeof(StatusType),leaveData[8]));
            }
            catch(IndexOutOfRangeException){
            return new Leave (Int32.Parse (leaveData[1]), leaveData[2], leaveData[3], leaveData[4], leaveData[5],DateTime.Parse(leaveData[6]),DateTime.Parse(leaveData[7]));
            }
        }
        public bool UpdateStatus(int leaveId, string status)
        {
            Leave leave = GetLeave(leaveId);
            string[] lines = File.ReadAllLines (this.destinationPath);
            for (int i = 2; i < lines.Length; i++) 
            {
                string[] leaveData = lines[i].Split ('|',StringSplitOptions.RemoveEmptyEntries);
                if(Int32.Parse(leaveData[0]) == leaveId)
                {
                    Console.WriteLine($"Status: {leaveData[8]}");
                    leave.SetStatus(status);
                    return true;
                }
            }
            return false;
        }
        public HashSet<Leave> LeaveByTitle(string title)
        {
            HashSet<Leave> list = new HashSet<Leave>();
            string[] lines = File.ReadAllLines (this.destinationPath);
            for (int i = 2; i < lines.Length; i++) 
            {
                string[] leaveData = lines[i].Split ('|',StringSplitOptions.RemoveEmptyEntries);
                if(leaveData[4] == title)
                {
                    list.Add(new Leave (Int32.Parse(leaveData[0]),Int32.Parse (leaveData[1]), leaveData[2], leaveData[3], leaveData[4], leaveData[5],DateTime.Parse(leaveData[6]),DateTime.Parse(leaveData[7]), (StatusType)Enum.Parse(typeof(StatusType),leaveData[8])));
                }
            }
            return list;
        }
        public HashSet<Leave> LeaveByStatus(string status)
        {    
            HashSet<Leave> list = new HashSet<Leave>();
            string[] lines = File.ReadAllLines (this.destinationPath);
            for (int i = 2; i < lines.Length; i++) 
            {
                string[] leaveData = lines[i].Split ('|',StringSplitOptions.RemoveEmptyEntries);
                if(leaveData[8] == status)
                {
                    list.Add(new Leave (Int32.Parse(leaveData[0]),Int32.Parse (leaveData[1]), leaveData[2], leaveData[3], leaveData[4], leaveData[5],DateTime.Parse(leaveData[6]),DateTime.Parse(leaveData[7]), (StatusType)Enum.Parse(typeof(StatusType),leaveData[8])));
                }
            }
            return list;
        }
    }
}

