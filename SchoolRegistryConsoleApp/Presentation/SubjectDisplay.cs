using Azure;
using Business;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegistryConsoleApp.Presentation
{
    public class SubjectDisplay : Display
    {
        private int closeOperationId = 6;
        private SubjectBusiness business = new SubjectBusiness();

        public SubjectDisplay()
        {
            Input();
        }

        public override void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(new string('=', 67));
            Console.WriteLine(@"███████╗██╗   ██╗██████╗      ██╗███████╗ ██████╗████████╗███████╗ 
██╔════╝██║   ██║██╔══██╗     ██║██╔════╝██╔════╝╚══██╔══╝██╔════╝ 
███████╗██║   ██║██████╔╝     ██║█████╗  ██║        ██║   ███████╗ 
╚════██║██║   ██║██╔══██╗██   ██║██╔══╝  ██║        ██║   ╚════██║ 
███████║╚██████╔╝██████╔╝╚█████╔╝███████╗╚██████╗   ██║   ███████║ 
╚══════╝ ╚═════╝ ╚═════╝  ╚════╝ ╚══════╝ ╚═════╝   ╚═╝   ╚══════╝ ");
            Console.WriteLine(new string('=', 67));
            Console.WriteLine("1. List all subjects" + new string(' ', 47));
            Console.WriteLine("2. Add new subject" + new string(' ', 49));
            Console.WriteLine("3. Update subject" + new string(' ', 50));
            Console.WriteLine("4. Fetch subject by ID" + new string(' ', 45));
            Console.WriteLine("5. Delete subject by ID" + new string(' ', 44));
            Console.WriteLine("6. Exit" + new string(' ', 60));
            Console.WriteLine("ENTER A COMMAND ID: " + new string(' ', 47));
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }


        public override void Add()
        {
            Console.Clear();
            ShowMenu();
            //Subject subject = new Subject();
            //Console.WriteLine("Enter name: ");
            //subject.Name = Console.ReadLine();
            //business.Add(subject);
            //Console.Clear();
            //ShowMenu();
            //Console.ForegroundColor = ConsoleColor.DarkRed;
            //Console.BackgroundColor = ConsoleColor.DarkGray;
            //Console.WriteLine($"Subject \"{subject.Name}\" added.");
            //Console.ForegroundColor = ConsoleColor.White;
            //Console.BackgroundColor = ConsoleColor.Black;

            string name = InputHelper.GetNonEmptyString("Enter subject's name:");

            var subject = new Subject
            {
                Name = name
            };

            business.Add(subject);
            Console.WriteLine("Subject added successfully!");
        }

        public override void ListAll()
        {
            Console.Clear();
            ShowMenu();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(new string('-', 24));
            Console.WriteLine(@"╔═╗╦ ╦╔╗  ╦╔═╗╔═╗╔╦╗╔═╗ 
╚═╗║ ║╠╩╗ ║║╣ ║   ║ ╚═╗ 
╚═╝╚═╝╚═╝╚╝╚═╝╚═╝ ╩ ╚═╝ "); 
            Console.WriteLine(new string('-', 24));
            var items = business.GetAll();
            if(items.Count == 0)
                Console.WriteLine("None                    ");
            else
                foreach (var item in items)
                    Console.WriteLine($"{item.Id,-5} {item.Name,15}   ");
            Console.WriteLine(new string('-', 24));
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
            //Subject subject = business.Get(id);
            //if (subject != null)
            //{
            //    Console.WriteLine("Enter name: ");
            //    subject.Name = Console.ReadLine();
            //    business.Update(subject);
            //    Console.Clear();
            //    ShowMenu();
            //    Console.ForegroundColor = ConsoleColor.DarkRed;
            //    Console.BackgroundColor = ConsoleColor.DarkGray;
            //    Console.WriteLine($"Subject with Id: {id} updated.");
            //    Console.ForegroundColor = ConsoleColor.White;
            //    Console.BackgroundColor = ConsoleColor.Black;
            //}
            //else
            //{
            //    Console.Clear();
            //    ShowMenu();
            //    Console.ForegroundColor = ConsoleColor.DarkRed;
            //    Console.BackgroundColor = ConsoleColor.DarkGray;
            //    Console.WriteLine($"Subject with Id: {id} not found");
            //    Console.ForegroundColor = ConsoleColor.White;
            //    Console.BackgroundColor = ConsoleColor.Black;
            //}
            ListAll();

            int id = InputHelper.GetValidInt("Enter ID to update:");
            Subject subject = business.Get(id);

            if (subject != null)
            {
                subject.Name = InputHelper.GetNonEmptyString("Enter subject's name:");              

                business.Update(subject);

                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Subject with ID: {id} updated successfully.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Subject with ID: {id} not found.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        public override void Fetch()
        {
            Console.Clear();
            ShowMenu();
            int id = 0;
            Console.WriteLine("All current subjects:");
            ListAll();
            Console.WriteLine("Enter ID to fetch: ");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            char pressedKey = keyInfo.KeyChar;
            if (char.IsDigit(pressedKey))
                id = int.Parse(pressedKey.ToString());
            Subject subject = business.Get(id);
            if (subject != null)
            {
                Console.WriteLine(subject);
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {               
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("There is no such subject to fetch, please choose valid ID");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        public override void Delete()
        {
            Console.Clear();
            ShowMenu();
            //int id = 0;
            //Console.WriteLine("All current subjects:");
            //ListAll();
            //Console.Write("Enter ID to delete: ");
            //ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            //char pressedKey = keyInfo.KeyChar;
            //if (char.IsDigit(pressedKey))
            //    id = int.Parse(pressedKey.ToString());
            //business.Delete(id);
            //Console.Clear();
            //ShowMenu();
            //Console.ForegroundColor = ConsoleColor.DarkRed;
            //Console.BackgroundColor = ConsoleColor.DarkGray;
            //Console.WriteLine($"Subject with id:{id} deleted");
            //Console.ForegroundColor = ConsoleColor.White;
            //Console.BackgroundColor = ConsoleColor.Black;

            ListAll();

            int id = InputHelper.GetValidInt("Enter ID to delete:");
            Subject subject = business.Get(id);

            if (subject != null)
            {
                business.Delete(id);
                Console.WriteLine($"Subject with ID: {id} deleted successfully.");
            }
            else
                Console.WriteLine($"Subject with ID: {id} not found.");
        }
    }
}
