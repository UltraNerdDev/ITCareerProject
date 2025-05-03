using Business;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SchoolRegistryConsoleApp.Presentation
{
    //Display class for Students, using the business layer to perform CRUD operations and enhancing the UI experience
    public class StudentsDisplay : Display
    {
        private int closeOperationId = 6;        
        private StudentBusiness studentBusiness = new StudentBusiness();
        private ParentBusiness  parentBusiness = new ParentBusiness();
        private ClassGroupBusiness groupBusiness = new ClassGroupBusiness();

        public StudentsDisplay()
        {
            Input();
        }

        //Shows the user menu of the given entity on the console
        public override void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(new string('=', 69));
            Console.WriteLine(@"███████╗████████╗██╗   ██╗██████╗ ███████╗███╗   ██╗████████╗███████╗
██╔════╝╚══██╔══╝██║   ██║██╔══██╗██╔════╝████╗  ██║╚══██╔══╝██╔════╝
███████╗   ██║   ██║   ██║██║  ██║█████╗  ██╔██╗ ██║   ██║   ███████╗
╚════██║   ██║   ██║   ██║██║  ██║██╔══╝  ██║╚██╗██║   ██║   ╚════██║
███████║   ██║   ╚██████╔╝██████╔╝███████╗██║ ╚████║   ██║   ███████║
╚══════╝   ╚═╝    ╚═════╝ ╚═════╝ ╚══════╝╚═╝  ╚═══╝   ╚═╝   ╚══════╝");
            Console.WriteLine(new string('=', 69));
            Console.WriteLine("1. List all students" + new string(' ', 49));
            Console.WriteLine("2. Add new student" + new string(' ', 51));
            Console.WriteLine("3. Update student" + new string(' ', 52));
            Console.WriteLine("4. Fetch student by ID" + new string(' ', 47));
            Console.WriteLine("5. Delete student by ID" + new string(' ', 46));
            Console.WriteLine("6. Exit" + new string(' ', 62));
            Console.WriteLine("ENTER A COMMAND ID: " + new string(' ', 49));
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        //Add method realising the logic of adding a new Student object to the database with UI
        public override void Add()
        {
            Console.Clear();
            ShowMenu();

            string firstName = InputHelper.GetNonEmptyString("Enter student's first name:");
            string lastName = InputHelper.GetNonEmptyString("Enter student's last name:");
            string email = InputHelper.GetNonEmptyString("Enter student's email:");
            Console.WriteLine("Enter student's age: (optional)");
            string ageString = Console.ReadLine()?.Trim();
            Console.WriteLine("Enter enrolment date (optional): YYYY");
            string dateString = Console.ReadLine()?.Trim();

            int? date = null;
            if(!string.IsNullOrWhiteSpace(dateString))
            {
                if (int.TryParse(dateString, out int parsedDate))
                    date = parsedDate;
                else
                    Console.WriteLine("Invalid date entered. Date will be left empty.");
            }

            int? age = null;
            if (!string.IsNullOrWhiteSpace(ageString))
            {
                if (int.TryParse(ageString, out int parsedAge))
                    age = parsedAge;
                else
                    Console.WriteLine("Invalid age entered. Age will be left empty.");
            }

            var omom = groupBusiness.GetAll();
            Console.WriteLine("Avalible classes:");
            Console.WriteLine(new string('-', 21));
            if (omom.Count == 0)
                Console.WriteLine("None");
            else
                foreach (var item in omom)
                    Console.WriteLine($"{item.Id,-5} {item.Name,7}");
            Console.WriteLine(new string('-', 21));
            // Get valid class group ID
            int classGroupId = InputHelper.GetValidForeignKey(
                "Enter class group ID:",
                context => context.Classes
            );

            var itim = parentBusiness.GetAll();
            Console.WriteLine("Avalible parents:");
            Console.WriteLine(new string('-', 21));
            if (itim.Count == 0)
                Console.WriteLine("None");
            else
                foreach (var item in itim)
                    Console.WriteLine($"{item.Id,-3} {item.FirstName,6} {item.LastName,6}");
            Console.WriteLine(new string('-', 21));
            // Get valid parent ID
            int parentId = InputHelper.GetValidForeignKey(
                "Enter parent ID:",
                context => context.Parents
            );

            var student = new Student
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Age = age,
                ClassGroupId = classGroupId,
                EnrollmentDate = date,
                ParentId = parentId
            };

            studentBusiness.Add(student);

            Console.Clear();
            ShowMenu();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Student added successfully.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        //ListAll method realising the logic of listing all of the Student objects in the database with UI
        public override void ListAll()
        {
            Console.Clear();
            ShowMenu();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(new string('-', 69));
            Console.WriteLine(@"╔═╗╔╦╗╦ ╦╔╦╗╔═╗╔╗╔╔╦╗╔═╗                                             
╚═╗ ║ ║ ║ ║║║╣ ║║║ ║ ╚═╗                                             
╚═╝ ╩ ╚═╝═╩╝╚═╝╝╚╝ ╩ ╚═╝                                             ");
            Console.WriteLine(new string('-', 69));
            var items = studentBusiness.GetAll();
            if (items.Count == 0)
                Console.WriteLine("No students found                                                    ");
            else
                foreach (var item in items)                   
                    Console.WriteLine(item);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(new string('-', 69));
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        //Update method realising the logic of updating an existing Student object in the database with UI
        public override void Update()
        {
            Console.Clear();
            ShowMenu();
            Console.WriteLine("All current students:");
            ListAll();

            int id = InputHelper.GetValidInt("Enter ID to update:");
            Student student = studentBusiness.Get(id);

            if (student != null)
            {
                student.FirstName = InputHelper.GetNonEmptyString("Enter student's first name:");
                student.LastName = InputHelper.GetNonEmptyString("Enter student's last name:");
                student.Email = InputHelper.GetNonEmptyString("Enter student's email:");
                student.Age = InputHelper.GetValidInt("Enter student's age:");

                //Displaying the available classes and parents
                var omom = groupBusiness.GetAll();
                Console.WriteLine("Avalible classes:");
                Console.WriteLine(new string('-', 21));
                if (omom.Count == 0)
                    Console.WriteLine("None");
                else
                    foreach (var item in omom)
                        Console.WriteLine($"{item.Id,-5} {item.Name,7}");
                Console.WriteLine(new string('-', 21));
                // Get valid class group ID
                student.ClassGroupId = InputHelper.GetValidForeignKey(
                    "Enter class group ID:",
                    context => context.Classes
                );

                var itim = parentBusiness.GetAll();
                Console.WriteLine("Avalible parents:");
                Console.WriteLine(new string('-', 21));
                if (itim.Count == 0)
                    Console.WriteLine("None");
                else
                    foreach (var item in itim)
                        Console.WriteLine($"{item.Id,-3} {item.FirstName,6} {item.LastName,6}");
                Console.WriteLine(new string('-', 21));
                // Get valid parent ID
                student.ParentId = InputHelper.GetValidForeignKey(
                    "Enter parent ID:",
                    context => context.Parents
                );

                studentBusiness.Update(student);

                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Student with ID: {id} updated successfully.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Student with ID: {id} not found.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        //Fetch method realising the logic of fetching a single Student object from the database with UI
        public override void Fetch()
        {
            Console.Clear();
            ShowMenu();
            Console.WriteLine("All current students:");
            ListAll();
            
            int id = InputHelper.GetValidInt("Enter ID to fetch:");
            Student student = studentBusiness.Get(id);
            if (student != null)
            {
                Console.WriteLine(student);
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("There is no such student to fetch, please choose valid ID");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        //Delete method realising the logic of deleting an existing Student object in the database with UI
        public override void Delete()
        {
            Console.Clear();
            ShowMenu();

            int id = InputHelper.GetValidInt("Enter ID to delete:");
            Student student = studentBusiness.Get(id);

            if (student != null)
            {
                studentBusiness.Delete(id);
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Student with ID: {id} deleted successfully.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Student with ID: {id} not found.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
    }
}
