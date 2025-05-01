using Business;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegistryConsoleApp.Presentation
{
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
        public override void Add()
        {
            Console.Clear();
            ShowMenu();
            Student student = new Student();
            Console.WriteLine("Enter First Name: ");
            student.FirstName = Console.ReadLine();
            Console.WriteLine("Enter Last Name: ");
            student.LastName = Console.ReadLine();
            Console.WriteLine("Ënter Age number: ");
            student.Age = int.Parse(Console.ReadLine());
            Console.WriteLine("Ënter Class Group ID: ");
            var items = groupBusiness.GetAll();
            if (items.Count == 0)
                Console.WriteLine("None");
            else
                foreach (var item in items)
                    Console.WriteLine($"{item.Id,-5} {item.Name,7} {item.Year}");
            Console.WriteLine("Avaliable class groups");
            student.ClassGroupId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter email: ");
            student.Email = Console.ReadLine();
            Console.WriteLine("Enter date: ");
            student.EnrollmentDate = DateOnly.Parse(Console.ReadLine());
            Console.WriteLine("Enter parent Id: ");
            var items2 = parentBusiness.GetAll();
            if (items2.Count == 0)
                Console.WriteLine("None");
            else
                foreach (var item in items2)
                    Console.WriteLine($"{item.Id,-5} {item.FirstName,15}");
            Console.WriteLine("Avaliable parents");
            student.ParentId = int.Parse(Console.ReadLine());
            studentBusiness.Add(student);
            Console.Clear();
            ShowMenu();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Student \"{student.FirstName}-{student.LastName}-{student.Age}-{student.ClassGroup}-{student.Email}-{student.EnrollmentDate}-{student.Parent}\" added.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public override void ListAll()
        {
            Console.Clear();
            ShowMenu();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(new string('-', 24));
            Console.WriteLine(@"╔═╗╔╦╗╦ ╦╔╦╗╔═╗╔╗╔╔╦╗╔═╗
╚═╗ ║ ║ ║ ║║║╣ ║║║ ║ ╚═╗
╚═╝ ╩ ╚═╝═╩╝╚═╝╝╚╝ ╩ ╚═╝");
            Console.WriteLine(new string('-', 24));
            var items = studentBusiness.GetAll();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            if (items.Count == 0)
                Console.WriteLine("No students found       ");
            else
                foreach (var item in items)
                    Console.WriteLine($"{item.Id,-5} {item.FirstName,4} {item.LastName,4} {item.Age}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(new string('-', 24));
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
            Student student = studentBusiness.Get(id);
            if (student != null)
            {
                Console.WriteLine("Enter First Name: ");
                student.FirstName = Console.ReadLine();
                Console.WriteLine("Enter Last Name: ");
                student.LastName = Console.ReadLine();
                Console.WriteLine("Ënter Age: ");
                student.Age = int.Parse(Console.ReadLine());
                Console.WriteLine("Ënter Class Group Id number: ");
                var itim = groupBusiness.GetAll();
                if (itim.Count == 0)
                    Console.WriteLine("None");
                else
                    foreach (var item in itim)
                        Console.WriteLine($"{item.Id,-5} {item.Name,15}");
                Console.WriteLine("Avaliable class groups");
                student.ClassGroupId = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter email: ");
                student.Email = Console.ReadLine();
                Console.WriteLine("Enter date: ");
                student.EnrollmentDate = DateOnly.Parse(Console.ReadLine());
                Console.WriteLine("Enter parent Id: ");
                var items = parentBusiness.GetAll();
                if (items.Count == 0)
                    Console.WriteLine("None");
                else
                    foreach (var item in items)
                        Console.WriteLine($"{item.Id,-5} {item.FirstName,15}");
                Console.WriteLine("Avaliable parents");
                student.ParentId = int.Parse(Console.ReadLine());
                studentBusiness.Update(student);
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Student with Id: {id} updated.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Student with Id: {id} not found");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
        public override void Fetch()
        {
            Console.Clear();
            ShowMenu();
            int id = 0;
            Console.WriteLine("All current Students:");
            ListAll();
            Console.WriteLine("Enter ID to fetch: ");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            char pressedKey = keyInfo.KeyChar;
            if (char.IsDigit(pressedKey))
                id = int.Parse(pressedKey.ToString());
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
        public override void Delete()
        {
            Console.Clear();
            ShowMenu();
            int id = 0;
            Console.WriteLine("All current students:");
            ListAll();
            Console.Write("Enter ID to delete: ");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            char pressedKey = keyInfo.KeyChar;
            if (char.IsDigit(pressedKey))
                id = int.Parse(pressedKey.ToString());
            studentBusiness.Delete(id);
            Console.Clear();
            ShowMenu();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Student with id:{id} deleted");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

    }
}
