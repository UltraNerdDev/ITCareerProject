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
            Teacher teacher = new Teacher();
            Console.WriteLine("Enter First Name: ");
            teacher.FirstName = Console.ReadLine();
            Console.WriteLine("Enter Last Name: ");
            teacher.LastName = Console.ReadLine();
            Console.WriteLine("Ënter Phone number: ");
            teacher.Phone = Console.ReadLine();
            Console.WriteLine("Enter email: ");
            teacher.Email = Console.ReadLine();
            Console.WriteLine("Enter subjectID: ");
            var items = subjectBusiness.GetAll();
            if(items.Count == 0)
                Console.WriteLine("None");
            else
                foreach (var item in items)
                    Console.WriteLine($"{item.Id,-5} {item.Name,15}");
            Console.WriteLine("Avaliable subjects");
            teacher.SubjectId = int.Parse(Console.ReadLine());
            teacherBusiness.Add(teacher);
            Console.Clear();
            ShowMenu();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Teacher \"{teacher.FirstName}-{teacher.LastName}-{teacher.Phone}-{teacher.Email}\" added.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
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
 ╩ ╚═╝╩ ╩╚═╝╩ ╩╚═╝╩╚═╚═╝");
            Console.WriteLine(new string('-', 66));
            var items = teacherBusiness.GetAll();
            if (items.Count == 0)
                Console.WriteLine("No teachers found       ");
            else
                foreach (var item in items)
                    Console.WriteLine($"{item.Id,-3} {item.FirstName,5} {item.LastName,5} {item.Phone,12} {item.Email, 14} {item.Subject.Name, 14}    ");
            Console.WriteLine(new string('-', 66));
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public override void Update()
        {
            Console.Clear();
            ShowMenu();
            int id = 0;
            ListAll();
            Console.WriteLine("Enter ID to update: ");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            char pressedKey = keyInfo.KeyChar;
            if (char.IsDigit(pressedKey))
                id = int.Parse(pressedKey.ToString());
            Teacher teacher = teacherBusiness.Get(id);
            if (teacher != null)
            {
                Console.WriteLine("Enter First Name: ");
                teacher.FirstName = Console.ReadLine();
                Console.WriteLine("Enter Last Name: ");
                teacher.LastName = Console.ReadLine();
                Console.WriteLine("Ënter Phone number: ");
                teacher.Phone = Console.ReadLine();
                Console.WriteLine("Enter email: ");
                teacher.Email = Console.ReadLine();
                Console.WriteLine("Enter subjectID: ");
                teacher.SubjectId = int.Parse(Console.ReadLine());
                teacherBusiness.Update(teacher);
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Parent with Id: {id} updated.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Parent with Id: {id} not found");
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
            int id = 0;
            Console.WriteLine("All current teachers:");
            ListAll();
            Console.Write("Enter ID to delete: ");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            char pressedKey = keyInfo.KeyChar;
            if (char.IsDigit(pressedKey))
                id = int.Parse(pressedKey.ToString());
            teacherBusiness.Delete(id);
            Console.Clear();
            ShowMenu();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Parent with id:{id} deleted");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
