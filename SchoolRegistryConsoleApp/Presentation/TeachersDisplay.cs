using Business;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegistryConsoleApp.Presentation
{
    internal class TeachersDisplay : Display
    {
        private int closeOperationId = 6;
        private TeacherBusiness teacherBusiness = new TeacherBusiness();
        private SubjectBusiness subjectBusiness = new SubjectBusiness();
        public TeachersDisplay()
        {
            Input();
        }
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
        
        public override void Add()
        {
            Console.Clear();
            ShowMenu();
            //Teacher teacher = new Teacher();
            //Console.WriteLine("Enter First Name: ");
            //teacher.FirstName = Console.ReadLine();
            //Console.WriteLine("Enter Last Name: ");
            //teacher.LastName = Console.ReadLine();
            //Console.WriteLine("Ënter Phone number: ");
            //teacher.Phone = Console.ReadLine();
            //Console.WriteLine("Enter email: ");
            //teacher.Email = Console.ReadLine();
            //Console.WriteLine("Enter subjectID: ");
            //var items = subjectBusiness.GetAll();
            //if(items.Count == 0)
            //    Console.WriteLine("None");
            //else
            //    foreach (var item in items)
            //        Console.WriteLine($"{item.Id,-5} {item.Name,15}");
            //Console.WriteLine("Avaliable subjects");
            //teacher.SubjectId = int.Parse(Console.ReadLine());
            //teacherBusiness.Add(teacher);
            //Console.Clear();
            //ShowMenu();
            //Console.ForegroundColor = ConsoleColor.DarkRed;
            //Console.BackgroundColor = ConsoleColor.DarkGray;
            //Console.WriteLine($"Teacher \"{teacher.FirstName}-{teacher.LastName}-{teacher.Phone}-{teacher.Email}\" added.");
            //Console.ForegroundColor = ConsoleColor.White;
            //Console.BackgroundColor = ConsoleColor.Black;

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
            Console.WriteLine("Teacher added successfully!");
        }

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
                Console.WriteLine("No teachers found       ");
            else
                foreach (var item in items)
                    Console.WriteLine(item);
            Console.WriteLine(new string('-', 66));
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public override void Update()
        {
            Console.Clear();
            ShowMenu();
            //int id = 0;
            //ListAll();
            //Console.WriteLine("Enter ID to update: ");
            //ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            //char pressedKey = keyInfo.KeyChar;
            //if (char.IsDigit(pressedKey))
            //    id = int.Parse(pressedKey.ToString());
            //Teacher teacher = teacherBusiness.Get(id);
            //if (teacher != null)
            //{
            //    Console.WriteLine("Enter First Name: ");
            //    teacher.FirstName = Console.ReadLine();
            //    Console.WriteLine("Enter Last Name: ");
            //    teacher.LastName = Console.ReadLine();
            //    Console.WriteLine("Ënter Phone number: ");
            //    teacher.Phone = Console.ReadLine();
            //    Console.WriteLine("Enter email: ");
            //    teacher.Email = Console.ReadLine();
            //    Console.WriteLine("Enter subjectID: ");
            //    teacher.SubjectId = int.Parse(Console.ReadLine());
            //    teacherBusiness.Update(teacher);
            //    Console.Clear();
            //    ShowMenu();
            //    Console.ForegroundColor = ConsoleColor.DarkRed;
            //    Console.BackgroundColor = ConsoleColor.DarkGray;
            //    Console.WriteLine($"Parent with Id: {id} updated.");
            //    Console.ForegroundColor = ConsoleColor.White;
            //    Console.BackgroundColor = ConsoleColor.Black;
            //}
            //else
            //{
            //    Console.Clear();
            //    ShowMenu();
            //    Console.ForegroundColor = ConsoleColor.DarkRed;
            //    Console.BackgroundColor = ConsoleColor.DarkGray;
            //    Console.WriteLine($"Parent with Id: {id} not found");
            //    Console.ForegroundColor = ConsoleColor.White;
            //    Console.BackgroundColor = ConsoleColor.Black;
            //}
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

                teacher.SubjectId = InputHelper.GetValidForeignKey(
                    "Enter subject ID:",
                    context => context.Subjects
                );

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
        public override void Fetch()
        {
            Console.Clear();
            ShowMenu();
            int id = 0;
            Console.WriteLine("All current Parents:");
            ListAll();
            Console.WriteLine("Enter ID to fetch: ");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            char pressedKey = keyInfo.KeyChar;
            if (char.IsDigit(pressedKey))
                id = int.Parse(pressedKey.ToString());
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
        public override void Delete()
        {
            Console.Clear();
            ShowMenu();
            //int id = 0;
            //Console.WriteLine("All current teachers:");
            //ListAll();
            //Console.Write("Enter ID to delete: ");
            //ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            //char pressedKey = keyInfo.KeyChar;
            //if (char.IsDigit(pressedKey))
            //    id = int.Parse(pressedKey.ToString());
            //teacherBusiness.Delete(id);
            //Console.Clear();
            //ShowMenu();
            //Console.ForegroundColor = ConsoleColor.DarkRed;
            //Console.BackgroundColor = ConsoleColor.DarkGray;
            //Console.WriteLine($"Parent with id:{id} deleted");
            //Console.ForegroundColor = ConsoleColor.White;
            //Console.BackgroundColor = ConsoleColor.Black;

            ListAll();

            int id = InputHelper.GetValidInt("Enter ID to delete:");
            Teacher teacher = teacherBusiness.Get(id);

            if (teacher != null)
            {
                teacherBusiness.Delete(id);
                Console.WriteLine($"Teacher with ID: {id} deleted successfully.");
            }
            else
                Console.WriteLine($"Teacher with ID: {id} not found.");
        }
    }
}
