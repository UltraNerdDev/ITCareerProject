using Business;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegistryConsoleApp.Presentation
{
    //Display class for Teachers, using the business layer to perform CRUD operations and enhancing the UI experience
    internal class TeachersDisplay : Display
    {
        private TeacherBusiness teacherBusiness = new TeacherBusiness();
        private SubjectBusiness subjectBusiness = new SubjectBusiness();

        public TeachersDisplay()
        {
            Input();
        }

        //Shows the user menu of the given entity on the console
        public override void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(new string('=', 66));
            Console.WriteLine(@"████████╗███████╗ █████╗  ██████╗██╗  ██╗███████╗██████╗ ███████╗ 
╚══██╔══╝██╔════╝██╔══██╗██╔════╝██║  ██║██╔════╝██╔══██╗██╔════╝ 
   ██║   █████╗  ███████║██║     ███████║█████╗  ██████╔╝███████╗ 
   ██║   ██╔══╝  ██╔══██║██║     ██╔══██║██╔══╝  ██╔══██╗╚════██║ 
   ██║   ███████╗██║  ██║╚██████╗██║  ██║███████╗██║  ██║███████║ 
   ╚═╝   ╚══════╝╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚══════╝ ");
            Console.WriteLine(new string('=', 66));
            Console.WriteLine("1. List all teachers" + new string(' ', 46));
            Console.WriteLine("2. Add new teacher" + new string(' ', 48));
            Console.WriteLine("3. Update teacher" + new string(' ', 49));
            Console.WriteLine("4. Fetch teacher by ID" + new string(' ', 44));
            Console.WriteLine("5. Delete teacher by ID" + new string(' ', 43));
            Console.WriteLine("6. Exit" + new string(' ', 59));
            Console.WriteLine("ENTER A COMMAND ID: " + new string(' ', 46));
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        //Add method realising the logic of adding a new Teacher object to the database with UI
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
                string firstName = InputHelper.GetNonEmptyString("Enter teacher's first name:");
                string lastName = InputHelper.GetNonEmptyString("Enter teacher's last name:");
                string email = InputHelper.GetNonEmptyString("Enter teacher's email:");
                Console.WriteLine("Enter teacher's phone (optional)");
                string phone = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(phone)) phone = "none";

                Console.WriteLine("Avalible subjects: ");
                var items = subjectBusiness.GetAll();
                if (items.Count == 0)
                    Console.WriteLine("None");
                else
                    foreach (var item in items)
                        Console.WriteLine($"{item.Id,-5} {item.Name,15}");
                // Get optional SubjectId
                Console.WriteLine("Enter subject ID (or press Enter to skip):");
                string subjectIdInput = Console.ReadLine()?.Trim();
                int? subjectId = null;
                if (!string.IsNullOrWhiteSpace(subjectIdInput) && int.TryParse(subjectIdInput, out int parsedSubjectId))
                {
                    // Validate the entered SubjectId
                    if (items.Any(s => s.Id == parsedSubjectId))
                        subjectId = parsedSubjectId;
                    else
                    {
                        Console.WriteLine("Invalid subject ID. Skipping subject assignment.");
                        subjectId = null;
                    }
                }

                var teacher = new Teacher
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Phone = phone,
                    SubjectId = subjectId
                };

                teacherBusiness.Add(teacher);

                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Teacher added successfully.");
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

        //Lists all teachers in the database
        public override void ListAll()
        {
            Console.Clear();
            ShowMenu();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(new string('-', 66));
            Console.WriteLine(@"╔╦╗╔═╗╔═╗╔═╗╦ ╦╔═╗╦═╗╔═╗                                          
 ║ ║╣ ╠═╣║  ╠═╣║╣ ╠╦╝╚═╗                                          
 ╩ ╚═╝╩ ╩╚═╝╩ ╩╚═╝╩╚═╚═╝                                          ");
            Console.WriteLine(new string('-', 66));
            var items = teacherBusiness.GetAll();
            if (items.Count == 0)
                Console.WriteLine("No teachers found                                                 ");
            else
                foreach (var item in items)
                    Console.WriteLine(item);
            Console.WriteLine(new string('-', 66));
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        //Update method realising the logic of updating a Teacher object in the database with UI
        public override void Update()
        {
            Console.Clear();
            ShowMenu();
            Console.WriteLine("All current teachers:");
            ListAll();

            int id = InputHelper.GetValidInt("Enter ID to update:");
            Teacher teacher = teacherBusiness.Get(id);

            if (teacher != null)
            {
                teacher.FirstName = InputHelper.GetNonEmptyString("Enter teacher's first name:");
                teacher.LastName = InputHelper.GetNonEmptyString("Enter teacher's last name:");
                teacher.Email = InputHelper.GetNonEmptyString("Enter teacher's email:");
                string phone = InputHelper.GetNonEmptyString("Enter teacher's phone (optional):");
                teacher.Phone = string.IsNullOrWhiteSpace(phone) ? null : phone;

                //Display available subjects
                var items = subjectBusiness.GetAll();
                Console.WriteLine("Avalible subjects:");
                Console.WriteLine(new string('-', 25));
                if (items.Count == 0)
                    Console.WriteLine("None");
                else
                    foreach (var item in items)
                        Console.WriteLine($"{item.Id,-5} {item.Name,15}"); //not using the overriden ToString method for good-looking reasons
                Console.WriteLine(new string('-', 25));
                // Get optional SubjectId
                Console.WriteLine("Enter subject ID (or press Enter to skip):");
                string subjectIdInput = Console.ReadLine()?.Trim();
                int? subjectId = null;
                if (!string.IsNullOrWhiteSpace(subjectIdInput) && int.TryParse(subjectIdInput, out int parsedSubjectId))
                {
                    // Validate the entered SubjectId
                    if (items.Any(s => s.Id == parsedSubjectId))
                        subjectId = parsedSubjectId;
                    else
                    {
                        Console.WriteLine("Invalid subject ID. Skipping subject assignment.");
                        subjectId = null;
                    }
                }
                teacher.SubjectId = subjectId;

                teacherBusiness.Update(teacher);

                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Teacher with ID: {id} updated successfully.");
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

        //Fetch method realising the logic of fetching a Teacher object from the database with UI
        public override void Fetch()
        {
            Console.Clear();
            ShowMenu();
            
            int id = InputHelper.GetValidInt("Enter ID to fetch:");
            Teacher teacher = teacherBusiness.Get(id);
            if (teacher != null)
            {
                Console.WriteLine(teacher);
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("There is no such teacher to fetch, please choose valid ID");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        //Delete method realising the logic of deleting a Teacher object from the database with UI
        public override void Delete()
        {
            Console.Clear();
            ShowMenu();
            Console.WriteLine("All current teachers:");
            ListAll();

            Console.WriteLine("Press ESC to cancel or ENTER to continue the delete operation:");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            // Check if ESC is pressed
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
                Teacher teacher = teacherBusiness.Get(id);
                if (teacher != null)
                {
                    teacherBusiness.Delete(id);
                    Console.Clear();
                    ShowMenu();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"Teacher with ID: {id} deleted successfully.");
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
