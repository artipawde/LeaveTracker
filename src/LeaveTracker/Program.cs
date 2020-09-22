using System;

namespace LeaveTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            FileReadWrite file = new FileReadWrite();
            LeaveManipulation leaveManipulation = new LeaveManipulation();
            Employee emp = new Employee();
            file.CreateFile();
            Leave leave = new Leave();
            file.AddDataToList();

            int id = 0;
            id = emp.CheckIdIsValid();

            if(id != 0) {
                int choice;
                while ((choice = GetChoice()) != 0) {
                        switch (choice) 
                        {
                            case 1:
                                leaveManipulation.AddingLeave(id);
                                break;
                            case 2:
                                leaveManipulation.ListMyLeaves(id);
                                break;
                            case 3:
                                leaveManipulation.UpdateLeaveStatus(id);
                                break;
                            case 4:
                                leaveManipulation.SearchLeaveByTitle();
                                break;
                            case 5:
                                leaveManipulation.SearchLeaveByStatus();
                                break;
                            default:
                                Console.WriteLine("Enter valid choice");
                                break;
                        }
                }
            } else {
                Console.WriteLine ("Login failed. Please Enter Correct Id");
            }
        }
            public static int GetChoice () {
            try
            {
                Console.WriteLine("=======================================================");
                Console.WriteLine("0.Exit");
                Console.WriteLine("1.Create Leave");
                Console.WriteLine("2.List My Leaves");
                Console.WriteLine("3.Update leave Status");
                Console.WriteLine("4.Search Leave By Title");
                Console.WriteLine("5.Search Leave By Status");
                Console.WriteLine("Enter your choice :");
                return int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Choice Please Choose Correct Choice");
                return 0;
            }
        
        }
    }
}
