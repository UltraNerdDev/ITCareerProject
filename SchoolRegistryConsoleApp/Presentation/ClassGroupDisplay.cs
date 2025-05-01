using Business;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegistryConsoleApp.Presentation
{
    public class ClassGroupDisplay : Display
    {
        private int closeOperationId = 6;
        private ClassGroupBusiness classBusiness = new ClassGroupBusiness();
        private TeacherBusiness teacherBusiness = new TeacherBusiness();
        

        public ClassGroupDisplay()
        {
            Input();
        }

        public override void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(new string('=', 67));
            Console.WriteLine(@" ██████╗██╗      █████╗ ███████╗███████╗███████╗███████╗           
██╔════╝██║     ██╔══██╗██╔════╝██╔════╝██╔════╝██╔════╝           
██║     ██║     ███████║███████╗███████╗█████╗  ███████╗           
██║     ██║     ██╔══██║╚════██║╚════██║██╔══╝  ╚════██║           
╚██████╗███████╗██║  ██║███████║███████║███████╗███████║           
 ╚═════╝╚══════╝╚═╝  ╚═╝╚══════╝╚══════╝╚══════╝╚══════╝           ");
            Console.WriteLine(new string('=', 67));
            Console.WriteLine("1. List all class groups" + new string(' ', 43));
            Console.WriteLine("2. Add new class group" + new string(' ', 45));
            Console.WriteLine("3. Update class group" + new string(' ', 46));
            Console.WriteLine("4. Fetch class group by ID" + new string(' ', 41));
            Console.WriteLine("5. Delete class group by ID" + new string(' ', 40));
            Console.WriteLine("6. Exit" + new string(' ', 60));
            Console.WriteLine("ENTER A COMMAND ID: " + new string(' ', 47));
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }


        public override void Add()
        {
            Console.Clear();
            ShowMenu();
            //           ClassGroup group = new ClassGroup();
            //           Console.WriteLine("Enter name: ");
            //           group.Name = Console.ReadLine();
            //           Console.WriteLine("Enter year: ");
            //           group.Year = int.Parse(Console.ReadLine());
            //           Console.WriteLine("Avaliable teachers:");
            //           Console.ForegroundColor = ConsoleColor.Blue;
            //           Console.BackgroundColor = ConsoleColor.White;
            //           Console.WriteLine(new string('-', 66));
            //           Console.WriteLine(@"╔╦╗╔═╗╔═╗╔═╗╦ ╦╔═╗╦═╗╔═╗
            //║ ║╣ ╠═╣║  ╠═╣║╣ ╠╦╝╚═╗
            //╩ ╚═╝╩ ╩╚═╝╩ ╩╚═╝╩╚═╚═╝");
            //           Console.WriteLine(new string('-', 66));
            //           var items = teacherBusiness.GetAll();
            //           if (items.Count == 0)
            //               Console.WriteLine("No teachers found       ");
            //           else
            //               foreach (var item in items)
            //                   Console.WriteLine($"{item.Id,-3} {item.FirstName,5} {item.LastName,5} {item.Phone,12} {item.Email,14} {item.Subject.Name,14}    ");
            //           Console.WriteLine(new string('-', 66));
            //           Console.WriteLine("Enter teacher ID:");
            //           ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            //           char pressedKey = keyInfo.KeyChar;
            //           if (char.IsDigit(pressedKey))
            //               group.TeacherId = int.Parse(pressedKey.ToString());
            //           classBusiness.Add(group);
            //           Console.Clear();
            //           ShowMenu();
            //           Console.ForegroundColor = ConsoleColor.DarkRed;
            //           Console.BackgroundColor = ConsoleColor.DarkGray;
            //           Console.WriteLine($"Class group \"{group.Name}\" added.");
            //           Console.ForegroundColor = ConsoleColor.White;
            //           Console.BackgroundColor = ConsoleColor.Black;

            string name = InputHelper.GetNonEmptyString("Enter class group's name:");
            int year = InputHelper.GetValidIntYear("Enter class group's year:");

            Console.WriteLine("Avalible teachers:");
            var items = teacherBusiness.GetAll();
            if (items.Count == 0)
                Console.WriteLine("None");
            else
                foreach (var item in items)
                    Console.WriteLine($"{item.Id,-5} {item.FirstName, 15}");

            int teachedId = InputHelper.GetValidForeignKey(
                    "Enter teacher ID:",
                    context => context.Teachers
                    );

            var classGroup = new ClassGroup
            {
                Name = name,
                Year = year,
                TeacherId = teachedId
            };

            classBusiness.Add(classGroup);
            Console.WriteLine("Class added successfully!");
        }

        public override void ListAll()
        {
            Console.Clear();
            ShowMenu();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(new string('-', 67));
            Console.WriteLine(@"╔═╗╦  ╔═╗╔═╗╔═╗╔═╗╔═╗                                              
║  ║  ╠═╣╚═╗╚═╗║╣ ╚═╗                                              
╚═╝╩═╝╩ ╩╚═╝╚═╝╚═╝╚═╝                                              ");
            Console.WriteLine(new string('-', 67));
            var items = classBusiness.GetAll();
            if(items.Count == 0)           
                Console.WriteLine("No class groups found    ");
            else
                foreach (var item in items)
                    //Console.WriteLine($"{item.Id,-5} {item.Name,5} {(item.Teacher != null ? item.Teacher.FirstName : "No Teacher"),12} ");
                    Console.WriteLine(item);
            Console.WriteLine(new string('-', 67));
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
            //ClassGroup classGroup = classBusiness.Get(id);
            //if (classGroup != null)
            //{
            //    Console.WriteLine("Enter name: ");
            //    classGroup.Name = Console.ReadLine();
            //    Console.WriteLine("Avaliable teachers");
            //    var items = teacherBusiness.GetAll();
            //    foreach (var item in items)
            //        Console.WriteLine($"{item.Id,-5} {item.FirstName,15}");
            //    Console.WriteLine("Enter teacher ID: ");
            //    classGroup.TeacherId = int.Parse(Console.ReadLine());
            //    classBusiness.Update(classGroup);
            //    Console.Clear();
            //    ShowMenu();
            //    Console.ForegroundColor = ConsoleColor.DarkRed;
            //    Console.BackgroundColor = ConsoleColor.DarkGray;
            //    Console.WriteLine($"Class with Id: {id} updated.");
            //    Console.ForegroundColor = ConsoleColor.White;
            //    Console.BackgroundColor = ConsoleColor.Black;
            //}
            //else
            //{
            //    Console.Clear();
            //    ShowMenu();
            //    Console.ForegroundColor = ConsoleColor.DarkRed;
            //    Console.BackgroundColor = ConsoleColor.DarkGray;
            //    Console.WriteLine($"Class with Id: {id} not found");
            //    Console.ForegroundColor = ConsoleColor.White;
            //    Console.BackgroundColor = ConsoleColor.Black;
            //}

            ListAll();

            int id = InputHelper.GetValidInt("Enter ID to update:");
            ClassGroup classGroup = classBusiness.Get(id);

            if (classGroup != null)
            {
                classGroup.Name = InputHelper.GetNonEmptyString("Enter class group's name:");
                classGroup.Year = InputHelper.GetValidInt("Enter class group's year:");

                Console.WriteLine("Avalible teachers:");
                var items = teacherBusiness.GetAll();
                if (items.Count == 0)
                    Console.WriteLine("None");
                else
                    foreach (var item in items)
                        Console.WriteLine($"{item.Id,-5} {item.FirstName,15}");

                classGroup.TeacherId = InputHelper.GetValidForeignKey(
                    "Enter teacher ID:",
                    context => context.Classes
                );

                classBusiness.Update(classGroup);

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

        public override void Fetch()
        {
            Console.Clear();
            ShowMenu();
            int id = 0;
            Console.WriteLine("All current Classes:");
            ListAll();
            Console.WriteLine("Enter ID to fetch: ");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            char pressedKey = keyInfo.KeyChar;
            if (char.IsDigit(pressedKey))
                id = int.Parse(pressedKey.ToString());
            ClassGroup classGroup = classBusiness.Get(id);
            if (classGroup != null)
            {
                Console.WriteLine(classGroup);
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("There is no such class group to fetch, please choose valid ID");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        public override void Delete()
        {
            Console.Clear();
            ShowMenu();
            //int id = 0;
            //Console.WriteLine("All current class groups:");
            //ListAll();
            //Console.Write("Enter ID to delete: ");
            //ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            //char pressedKey = keyInfo.KeyChar;
            //if (char.IsDigit(pressedKey))
            //    id = int.Parse(pressedKey.ToString());
            //classBusiness.Delete(id);
            //Console.Clear();
            //ShowMenu();
            //Console.ForegroundColor = ConsoleColor.DarkRed;
            //Console.BackgroundColor = ConsoleColor.DarkGray;
            //Console.WriteLine($"Class group with id:{id} deleted");
            //Console.ForegroundColor = ConsoleColor.White;
            //Console.BackgroundColor = ConsoleColor.Black;

            ListAll();

            int id = InputHelper.GetValidInt("Enter ID to delete:");
            ClassGroup classgroup = classBusiness.Get(id);

            if (classgroup != null)
            {
                classBusiness.Delete(id);
                Console.WriteLine($"Class with ID: {id} deleted successfully.");
            }
            else
                Console.WriteLine($"Class with ID: {id} not found.");
        }
    }
}
