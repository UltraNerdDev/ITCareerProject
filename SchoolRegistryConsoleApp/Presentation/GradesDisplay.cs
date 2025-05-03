using Business;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegistryConsoleApp.Presentation
{
    //Display class for Grades, using the business layer to perform CRUD operations and enhancing the UI experience
    public class GradesDisplay : Display
    {
        private TeacherBusiness teacherBusiness = new TeacherBusiness();
        private SubjectBusiness subjectBusiness = new SubjectBusiness();
        private StudentBusiness studentBusiness = new StudentBusiness();
        private GradeBusiness gradeBusiness = new GradeBusiness();

        public GradesDisplay()
        {
            Input();
        }

        //Shows the user menu of the given entity on the console
        public override void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(new string('=', 49));
            Console.WriteLine(@" ██████╗ ██████╗  █████╗ ██████╗ ███████╗███████╗
██╔════╝ ██╔══██╗██╔══██╗██╔══██╗██╔════╝██╔════╝
██║  ███╗██████╔╝███████║██║  ██║█████╗  ███████╗
██║   ██║██╔══██╗██╔══██║██║  ██║██╔══╝  ╚════██║
╚██████╔╝██║  ██║██║  ██║██████╔╝███████╗███████║
 ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚═════╝ ╚══════╝╚══════╝");
            Console.WriteLine(new string('=', 49));
            Console.WriteLine("1. List all grades" + new string(' ', 31));
            Console.WriteLine("2. Add new grade" + new string(' ', 33));
            Console.WriteLine("3. Update grade" + new string(' ', 34));
            Console.WriteLine("4. Fetch grade by ID" + new string(' ', 29));
            Console.WriteLine("5. Delete grade by ID" + new string(' ', 28));
            Console.WriteLine("6. Exit" + new string(' ', 42));
            Console.WriteLine("ENTER A COMMAND ID: " + new string(' ', 29));
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        //Add method realising the logic of adding a new Grades object to the database with UI
        public override void Add()
        {
            Console.Clear();
            ShowMenu();

            Console.WriteLine("Press ESC to cancel or ENTER to continue the add operation:");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Add operation canceled.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                return;
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                //Get valid grade value (2-6)
                double gradeValue;
                while (true)
                {
                    gradeValue = double.Parse(InputHelper.GetNonEmptyString("Enter grade value (2-6):"));
                    if (gradeValue >= 2 && gradeValue <= 6)
                        break;
                    Console.WriteLine("Invalid grade value. Please enter a value between 2 and 6.");
                }

                //Get date
                DateOnly gradeDate = DateOnly.Parse(InputHelper.GetNonEmptyString("Enter grade date (dd.MM.yyyy):"));

                //Get comment (optional)
                Console.WriteLine("Enter comment (optional):");
                string comment = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(comment)) comment = "none";

                //Displaying the available students, subjects and teachers
                var itim = studentBusiness.GetAll();
                Console.WriteLine("Avalible students:");
                Console.WriteLine(new string('-', 25));
                if (itim.Count == 0)
                    Console.WriteLine("None");
                else
                    foreach (var item in itim)
                        Console.WriteLine($"{item.Id,-5} {item.FirstName,7} {item.LastName}");
                Console.WriteLine(new string('-', 25));
                // Get valid student ID
                int studentId = InputHelper.GetValidForeignKey(
                    "Enter student ID:",
                    context => context.Students
                );

                var items = subjectBusiness.GetAll();
                Console.WriteLine("Avalible subjects:");
                Console.WriteLine(new string('-', 25));
                if (items.Count == 0)
                    Console.WriteLine("None");
                else
                    foreach (var item in items)
                        Console.WriteLine($"ID: {item.Id,-5} {item.Name,15}");
                Console.WriteLine(new string('-', 25));
                // Get valid subject ID
                int subjectId = InputHelper.GetValidForeignKey(
                    "Enter subject ID:",
                    context => context.Subjects
                );

                var omom = teacherBusiness.GetAll();
                Console.WriteLine("Avalible teachers:");
                Console.WriteLine(new string('-', 25));
                if (omom.Count == 0)
                    Console.WriteLine("None");
                else
                    foreach (var item in omom)
                        Console.WriteLine($"ID: {item.Id,-5} {item.FirstName,7} {item.LastName}");
                Console.WriteLine(new string('-', 25));
                // Get valid teacher ID
                int teacherId = InputHelper.GetValidForeignKey(
                    "Enter teacher ID:",
                    context => context.Teachers
                );

                // Create and add the grade
                var grade = new Grade
                {
                    Value = gradeValue,
                    Date = gradeDate,
                    Comment = comment,
                    StudentId = studentId,
                    SubjectId = subjectId,
                    TeacherId = teacherId
                };

                gradeBusiness.Add(grade);

                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Grade added successfully.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Invalid key pressed. Add operation canceled.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        //ListAll method realising the logic of listing all of the Grades objects in the database with UI
        public override void ListAll()
        {
            Console.Clear();
            ShowMenu();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(new string('-', 49));
            Console.WriteLine(@"╔═╗╦═╗╔═╗╔╦╗╔═╗╔═╗                               
║ ╦╠╦╝╠═╣ ║║║╣ ╚═╗                               
╚═╝╩╚═╩ ╩═╩╝╚═╝╚═╝                               ");
            Console.WriteLine(new string('-', 49));
            var items = gradeBusiness.GetAll();
            if (items.Count == 0)
                Console.WriteLine("No grades found                                  ");
            else
                foreach (var item in items)
                    Console.WriteLine(item);
            Console.WriteLine(new string('-', 49));
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        //Update method realising the logic of updating existing Grades object in the database with UI
        public override void Update()
        {
            Console.Clear();
            ShowMenu();
            Console.WriteLine("All current grades:");
            ListAll();

            int id = InputHelper.GetValidInt("Enter ID to update:");
            Grade grade = gradeBusiness.Get(id);

            if (grade != null)
            {
                double gradeValue;
                //
                //Get valid grade value (2-6)
                while (true)
                {
                    gradeValue = double.Parse(InputHelper.GetNonEmptyString("Enter grade value (2-6):"));
                    if (gradeValue >= 2 && gradeValue <= 6)
                        break;
                    Console.WriteLine("Invalid grade value. Please enter a value between 2 and 6.");
                }
                grade.Value = gradeValue;

                //Get date
                grade.Date = DateOnly.Parse(InputHelper.GetNonEmptyString("Enter grade date (dd.MM.yyyy):"));

                //Get comment (optional)
                Console.WriteLine("Enter comment (optional):");
                string comment = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(comment)) comment = "None";
                grade.Comment = comment;

                //Displaying the available students, subjects and teachers
                var itim = studentBusiness.GetAll();
                Console.WriteLine("Avalible students:");
                Console.WriteLine(new string('-', 25));
                if (itim.Count == 0)
                    Console.WriteLine("None");
                else
                    foreach (var item in itim)
                        Console.WriteLine($"{item.Id,-5} {item.FirstName,7} {item.LastName}");
                Console.WriteLine(new string('-', 25));
                // Get valid student ID
                grade.StudentId = InputHelper.GetValidForeignKey(
                    "Enter student ID:",
                    context => context.Students
                );

                var items = subjectBusiness.GetAll();
                Console.WriteLine("Avalible subjects:");
                Console.WriteLine(new string('-', 25));
                if (items.Count == 0)
                    Console.WriteLine("None");
                else
                    foreach (var item in items)
                        Console.WriteLine($"ID: {item.Id,-5} {item.Name,15}");
                Console.WriteLine(new string('-', 25));
                // Get valid subject ID
                grade.SubjectId = InputHelper.GetValidForeignKey(
                    "Enter subject ID:",
                    context => context.Subjects
                );

                var omom = teacherBusiness.GetAll();
                Console.WriteLine("Avalible teachers:");
                Console.WriteLine(new string('-', 25));
                if (omom.Count == 0)
                    Console.WriteLine("None");
                else
                    foreach (var item in omom)
                        Console.WriteLine($"ID: {item.Id,-5} {item.FirstName,7} {item.LastName}");
                Console.WriteLine(new string('-', 25));
                // Get valid teacher ID
                grade.TeacherId = InputHelper.GetValidForeignKey(
                    "Enter teacher ID:",
                    context => context.Teachers
                );

                gradeBusiness.Update(grade);

                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Grade with ID: {id} updated successfully.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Teacher with ID: {id} not found.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        //Fetch method realising the logic of fetching a single Grades object from the database with UI
        public override void Fetch()
        {
            Console.Clear();
            ShowMenu();

            int id = InputHelper.GetValidInt("Enter ID to fetch:");
            Grade grade = gradeBusiness.Get(id);
            if (grade != null)
            {
                Console.WriteLine(grade);
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("There is no such grade to fetch, please choose valid ID");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        //Delete method realising the logic of deleting a single Grades object from the database with UI
        public override void Delete()
        {
            Console.Clear();
            ShowMenu();
            Console.WriteLine("All current grades:");
            ListAll();

            Console.WriteLine("Press ESC to cancel or ENTER to continue the delete operation:");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.WriteLine("Delete operation canceled.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                return;
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                int id = InputHelper.GetValidInt("Enter ID to delete:");
                Grade grade = gradeBusiness.Get(id);
                if (grade != null)
                {
                    gradeBusiness.Delete(id);
                    Console.Clear();
                    ShowMenu();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"Grade with ID: {id} deleted successfully.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.Clear();
                    ShowMenu();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"Grade with ID: {id} not found.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
            else
            {
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Invalid key pressed. Delete operation canceled.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
    }
}
