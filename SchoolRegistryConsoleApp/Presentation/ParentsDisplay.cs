using Data.Models;
using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegistryConsoleApp.Presentation
{
    public class ParentsDisplay : Display
    {
        private int closeOperationId = 6;
        private ParentBusiness Business = new ParentBusiness();
        public ParentsDisplay()
        {
            Input();
        }
        public override void ShowMenu()
        {
            Console.WriteLine(new string('=', 67));
            Console.WriteLine("Parents");
            Console.WriteLine(new string('=', 67));
            Console.WriteLine("1. List all parents" + new string(' ', 47));
            Console.WriteLine("2. Add new parent" + new string(' ', 49));
            Console.WriteLine("3. Update parent" + new string(' ', 50));
            Console.WriteLine("4. Fetch parent by ID" + new string(' ', 45));
            Console.WriteLine("5. Delete parent by ID" + new string(' ', 44));
            Console.WriteLine("6. Exit" + new string(' ', 60));
            Console.WriteLine("ENTER A COMMAND ID: " + new string(' ', 47));
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public override void Add()
        {
            Parent parent = new Parent();
            Console.WriteLine("Enter First Name: ");
            parent.FirstName = Console.ReadLine();
            Console.WriteLine("Enter Last Name: ");
            parent.LastName = Console.ReadLine();
            Console.WriteLine("Ënter Phone number: ");
            parent.PhoneNumber = Console.ReadLine();
            Console.WriteLine("Enter email: ");
            parent.Email = Console.ReadLine();
            Business.Add(parent);
            Console.Clear();
            ShowMenu();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Parent \"{parent.FirstName}-{parent.LastName}-{parent.PhoneNumber}-{parent.Email}\" added.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public override void ListAll()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(new string('-', 25));
            Console.WriteLine("PARENTS");
            Console.WriteLine(new string('-', 25));
            var products = Business.GetAll();
            foreach (var item in products)
                Console.WriteLine($"{item.Id,-5} {item.FirstName,15}    ");
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
            Parent parent = Business.Get(id);
            if (parent != null)
            {
                Console.WriteLine("Enter First Name: ");
                parent.FirstName = Console.ReadLine();               
                Console.WriteLine("Enter Last Name: ");
                parent.LastName = Console.ReadLine();
                Console.WriteLine("Ënter Phone number: ");
                parent.PhoneNumber = Console.ReadLine();
                Console.WriteLine("Enter email: ");
                parent.Email = Console.ReadLine();
                Business.Update(parent);
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
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);  // 'true' скрива натиснатия клавиш от екрана
            char pressedKey = keyInfo.KeyChar;
            // Преобразуваме натиснатия клавиш в int (ако е цифра)
            if (char.IsDigit(pressedKey))
                id = int.Parse(pressedKey.ToString());
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.White;
            Parent parent = Business.Get(id);
            if (parent != null)
            {
                Console.WriteLine(@$"ID: {id} First Name: {parent.FirstName}  Last Name: {parent.LastName}
Phone number: {parent.PhoneNumber}
Email: {parent.Email}");
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
                Console.WriteLine("There is no such parent to fetch, please choose valid ID");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
        public override void Delete()
        {
            int id = 0;
            Console.WriteLine("All current Parents:");
            ListAll();
            Console.Write("Enter ID to delete: ");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);  // 'true' скрива натиснатия клавиш от екрана
            char pressedKey = keyInfo.KeyChar;
            // Преобразуваме натиснатия клавиш в int (ако е цифра)
            if (char.IsDigit(pressedKey))
                id = int.Parse(pressedKey.ToString());
            Business.Delete(id);
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
