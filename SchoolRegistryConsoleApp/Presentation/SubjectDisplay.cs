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
            Subject subject = new Subject();
            Console.WriteLine("Enter name: ");
            subject.Name = Console.ReadLine();
            business.Add(subject);
            Console.Clear();
            ShowMenu();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Subject \"{subject.Name}\" added.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public override void ListAll()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(new string('-', 25));
            Console.WriteLine(@"╔═╗╦ ╦╔╗  ╦╔═╗╔═╗╔╦╗╔═╗  
╚═╗║ ║╠╩╗ ║║╣ ║   ║ ╚═╗  
╚═╝╚═╝╚═╝╚╝╚═╝╚═╝ ╩ ╚═╝  "); 
            Console.WriteLine(new string('-', 25));
            var products = business.GetAll();
            foreach (var item in products)
                Console.WriteLine($"{item.Id, -5} {item.Name, 15}    ");
            Console.WriteLine(new string('-', 25));
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public override void Update()
        {
            int id = 0;
            ListAll();
            Console.WriteLine("Enter ID to update: ");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);  // 'true' скрива натиснатия клавиш от екрана
            char pressedKey = keyInfo.KeyChar;
            // Преобразуваме натиснатия клавиш в int (ако е цифра)
            if (char.IsDigit(pressedKey))
                id = int.Parse(pressedKey.ToString());
            Subject subject = business.Get(id);
            if (subject != null)
            {
                Console.WriteLine("Enter name: ");
                subject.Name = Console.ReadLine();
                business.Update(subject);
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Subject with Id: {id} updated.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Subject with Id: {id} not found");
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
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);  // 'true' скрива натиснатия клавиш от екрана
            char pressedKey = keyInfo.KeyChar;
            // Преобразуваме натиснатия клавиш в int (ако е цифра)
            if (char.IsDigit(pressedKey))
                id = int.Parse(pressedKey.ToString());
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.White;
            Subject subject = business.Get(id);
            if (subject != null)
            {
                Console.WriteLine($"ID: {id} Name: {subject.Name}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
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
            int id = 0;
            Console.WriteLine("All current subjects:");
            ListAll();
            Console.Write("Enter ID to delete: ");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);  // 'true' скрива натиснатия клавиш от екрана
            char pressedKey = keyInfo.KeyChar;
            // Преобразуваме натиснатия клавиш в int (ако е цифра)
            if (char.IsDigit(pressedKey))
                id = int.Parse(pressedKey.ToString());
            business.Delete(id);
            Console.Clear();
            ShowMenu();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Subject with id:{id} deleted");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
