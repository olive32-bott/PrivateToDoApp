using System;
using System.Collections.Generic;
using OliviaPrivate.ToDoApp.Models;

namespace OliviaPrivate.ToDoApp
{
    class Program
    {
        static List<User> users = new List<User>();
        static List<ToDoTask> tasks = new List<ToDoTask>();
        
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Add New User");
                Console.WriteLine("2. Add New Task");
                Console.WriteLine("3. Assign Task to User");
                Console.WriteLine("4. Display Users and Tasks");
                Console.WriteLine("5. Exit");
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1": AddUser(); break;
                    case "2": AddTask(); break;
                    case "3": AssignTask(); break;
                    case "4": 
                        Console.WriteLine("Displaying all users and tasks...");
                        Console.Out.Flush(); 
                        DisplayData();
                        PromptForDataIfEmpty(); 
                        break;
                    case "5": return;
                    default: Console.WriteLine("Invalid choice. Try again."); break;
                }
            }
        }

        static void AddUser()
        {
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();
            
            users.Add(new User { Id = users.Count + 1, Username = username, Email = email, Password = password });
            Console.WriteLine("User added successfully!");
        }
        
        static void AddTask()
        {
            Console.Write("Enter Task Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Description: ");
            string description = Console.ReadLine();
            Console.Write("Enter Due Date (YYYY-MM-DD): ");
            DateTime dueDate = DateTime.Parse(Console.ReadLine());
            
            tasks.Add(new ToDoTask { Id = tasks.Count + 1, Name = name, Description = description, DueDate = dueDate, DateCreated = DateTime.Now });
            Console.WriteLine("Task added successfully!");
        }
        
        static void AssignTask()
        {
            if (users.Count == 0)
            {
                Console.WriteLine("No users available. You need to add a user before assigning tasks.");
                AddUser();
                return;
            }

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available. You need to add a task before assigning to a user.");
                AddTask();
                return;
            }

            Console.WriteLine("Select a Task ID:");
            foreach (var task in tasks) Console.WriteLine($"{task.Id}. {task.Name}");
            
            if (!int.TryParse(Console.ReadLine(), out int taskId) || taskId < 1 || taskId > tasks.Count)
            {
                Console.WriteLine("Invalid task ID. Returning to menu.");
                return;
            }

            Console.WriteLine("Select a User ID:");
            foreach (var user in users) Console.WriteLine($"{user.Id}. {user.Username}");
            
            if (!int.TryParse(Console.ReadLine(), out int userId) || userId < 1 || userId > users.Count)
            {
                Console.WriteLine("Invalid user ID. Returning to menu.");
                return;
            }

            tasks[taskId - 1].AssignedTo = users[userId - 1];
            Console.WriteLine($"Task '{tasks[taskId - 1].Name}' assigned to {users[userId - 1].Username} successfully!");
        }
        
        static void DisplayData()
        {
            try
            {
                Console.WriteLine("\nUsers:");
                Console.WriteLine("ID | Username | Email");
                foreach (var user in users) Console.WriteLine($"{user.Id} | {user.Username} | {user.Email}");

                Console.WriteLine("\nToDoTask:");
                Console.WriteLine("ID | Name | Description | Due Date | Assigned To");
                foreach (var task in tasks) 
                    Console.WriteLine($"{task.Id} | {task.Name} | {task.Description} | {task.DueDate.ToShortDateString()} | {(task.AssignedTo?.Username ?? "Unassigned")}");
                
                Console.Out.Flush(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying data: {ex.Message}");
            }
        }

        static void PromptForDataIfEmpty()
        {
            if (users.Count == 0 && tasks.Count == 0)
            {
                Console.WriteLine("\nNo users or tasks found.");
                Console.Write("Would you like to add a user or task? (1: Add User, 2: Add Task, 3: Return to Menu): ");
            }
            else if (users.Count == 0)
            {
                Console.WriteLine("\nNo users found. Please add a user.");
                AddUser();
                return;
            }
            else if (tasks.Count == 0)
            {
                Console.WriteLine("\nNo tasks found. Please add a task.");
                AddTask();
                return;
            }

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": AddUser(); break;
                case "2": AddTask(); break;
                case "3": return;
                default: Console.WriteLine("Invalid choice. Returning to menu."); break;
            }
        }
    }
}
