using Business;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegistryConsoleApp.Presentation
{
    public class GradesDisplay : Display
    {
        private int closeOperationId = 6;
        private TeacherBusiness teacherBusiness = new TeacherBusiness();
        private SubjectBusiness subjectBusiness = new SubjectBusiness();
        private StudentBusiness studentBusiness = new StudentBusiness();
        private GradeBusiness gradeBusiness = new GradeBusiness();
        public GradesDisplay()
        {
            Input();
        }
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
        public override void Add()
        {
            Console.Clear();
            ShowMenu();
            Grade grade = new Grade();
            Console.WriteLine("Enter Grade (2-6): ");
            grade.Value = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Date: ");
            grade.Date = DateOnly.Parse(Console.ReadLine());
            Console.WriteLine("Ënter Comment: ");
            grade.Comment = Console.ReadLine();
            Console.WriteLine("Enter Student Id: ");
            var itim = studentBusiness.GetAll();
            if (itim.Count == 0)
                Console.WriteLine("None");
            else
                foreach (var item in itim)
                    Console.WriteLine($"{item.Id,-5} {item.FirstName,7} {item.LastName}");
            Console.WriteLine("Avaliable students");
            grade.StudentId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter subjectID: ");
            var items = subjectBusiness.GetAll();
            if (items.Count == 0)
                Console.WriteLine("None");
            else
                foreach (var item in items)
                    Console.WriteLine($"{item.Id,-5} {item.Name,15}");
            Console.WriteLine("Avaliable subjects");
            grade.SubjectId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter teacherId: ");
            var omom = teacherBusiness.GetAll();
            if (omom.Count == 0)
                Console.WriteLine("None");
            else
                foreach (var item in omom)
                    Console.WriteLine($"{item.Id,-5} {item.FirstName,7} {item.LastName}");
            Console.WriteLine("Avaliable teachers");
            grade.TeacherId = int.Parse(Console.ReadLine());
            gradeBusiness.Add(grade);
            Console.Clear();
            ShowMenu();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Grade \"{grade.Value}-{grade.Date}-{grade.Student}-{grade.Subject}-{grade.Teacher}\" added.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public override void ListAll()
        {
            Console.Clear();
            ShowMenu();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(new string('-', 20));
            Console.WriteLine(@"╔═╗╦═╗╔═╗╔╦╗╔═╗╔═╗  
║ ╦╠╦╝╠═╣ ║║║╣ ╚═╗  
╚═╝╩╚═╩ ╩═╩╝╚═╝╚═╝  ");
            Console.WriteLine(new string('-', 20));
            var items = gradeBusiness.GetAll();
            if (items.Count == 0)
                Console.WriteLine("No grades found     ");
            else
                foreach (var item in items)
                    Console.WriteLine($"{item.Id,-5} {item.Value,7} {item.Date,1} {item.Comment,1} {item.Student,1} {item.Subject,1} {item.Teacher,1}");
            Console.WriteLine(new string('-', 20));
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
            Grade grade = gradeBusiness.Get(id);
            if (grade != null)
            {
                Console.WriteLine("Enter Grade (2-6): ");
                grade.Value = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Date: ");
                grade.Date = DateOnly.Parse(Console.ReadLine());
                Console.WriteLine("Ënter Comment: ");
                grade.Comment = Console.ReadLine();
                Console.WriteLine("Enter Student Id: ");
                var itim = studentBusiness.GetAll();
                if (itim.Count == 0)
                    Console.WriteLine("None");
                else
                    foreach (var item in itim)
                        Console.WriteLine($"{item.Id,-5} {item.FirstName,7} {item.LastName}");
                Console.WriteLine("Avaliable students");
                grade.StudentId = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter subjectID: ");
                var items = subjectBusiness.GetAll();
                if (items.Count == 0)
                    Console.WriteLine("None");
                else
                    foreach (var item in items)
                        Console.WriteLine($"{item.Id,-5} {item.Name,15}");
                Console.WriteLine("Avaliable subjects");
                grade.SubjectId = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter teacherId: ");
                var omom = teacherBusiness.GetAll();
                if (omom.Count == 0)
                    Console.WriteLine("None");
                else
                    foreach (var item in omom)
                        Console.WriteLine($"{item.Id,-5} {item.FirstName,7} {item.LastName}");
                Console.WriteLine("Avaliable teachers");
                grade.TeacherId = int.Parse(Console.ReadLine());
                gradeBusiness.Update(grade);
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Grade with Id: {id} updated.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Grade with Id: {id} not found");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
        public override void Fetch()
        {
            Console.Clear();
            ShowMenu();
            int id = 0;
            Console.WriteLine("All current Grades:");
            ListAll();
            Console.WriteLine("Enter ID to fetch: ");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            char pressedKey = keyInfo.KeyChar;
            if (char.IsDigit(pressedKey))
                id = int.Parse(pressedKey.ToString());
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
        public override void Delete()
        {
            Console.Clear();
            ShowMenu();
            int id = 0;
            Console.WriteLine("All current grades:");
            ListAll();
            Console.Write("Enter ID to delete: ");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            char pressedKey = keyInfo.KeyChar;
            if (char.IsDigit(pressedKey))
                id = int.Parse(pressedKey.ToString());
            gradeBusiness.Delete(id);
            Console.Clear();
            ShowMenu();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Grade with id:{id} deleted");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
