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
        private ParentBusiness business = new ParentBusiness();
        public ParentsDisplay()
        {
            Input();
        }
        public override void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(new string('=', 60));
            Console.WriteLine(@"██████╗  █████╗ ██████╗ ███████╗███╗   ██╗████████╗███████╗ 
██╔══██╗██╔══██╗██╔══██╗██╔════╝████╗  ██║╚══██╔══╝██╔════╝ 
██████╔╝███████║██████╔╝█████╗  ██╔██╗ ██║   ██║   ███████╗ 
██╔═══╝ ██╔══██║██╔══██╗██╔══╝  ██║╚██╗██║   ██║   ╚════██║ 
██║     ██║  ██║██║  ██║███████╗██║ ╚████║   ██║   ███████║ 
╚═╝     ╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═══╝   ╚═╝   ╚══════╝ ");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine("1. List all parents" + new string(' ', 41));
            Console.WriteLine("2. Add new parent" + new string(' ', 43));
            Console.WriteLine("3. Update parent" + new string(' ', 44));
            Console.WriteLine("4. Fetch parent by ID" + new string(' ', 39));
            Console.WriteLine("5. Delete parent by ID" + new string(' ', 38));
            Console.WriteLine("6. Exit" + new string(' ', 53));
            Console.WriteLine("ENTER A COMMAND ID: " + new string(' ', 40));
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public override void Add()
        {
            Console.Clear();
            ShowMenu();
            //Parent parent = new Parent();
            //Console.WriteLine("Enter First Name: ");
            //parent.FirstName = Console.ReadLine();
            //Console.WriteLine("Enter Last Name: ");
            //parent.LastName = Console.ReadLine();
            //Console.WriteLine("Ënter Phone number: ");
            //parent.PhoneNumber = Console.ReadLine();
            //Console.WriteLine("Enter email: ");
            //parent.Email = Console.ReadLine();
            //business.Add(parent);
            //Console.Clear();
            //ShowMenu();
            //Console.ForegroundColor = ConsoleColor.DarkRed;
            //Console.BackgroundColor = ConsoleColor.DarkGray;
            //Console.WriteLine($"Parent \"{parent.FirstName}-{parent.LastName}-{parent.PhoneNumber}-{parent.Email}\" added.");
            //Console.ForegroundColor = ConsoleColor.White;
            //Console.BackgroundColor = ConsoleColor.Black;

            string firstName = InputHelper.GetNonEmptyString("Enter parent's first name:");
            string lastName = InputHelper.GetNonEmptyString("Enter parent's last name:");
            Console.WriteLine("Enter parent's phone (optional): ");
            string phone = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(phone)) phone = null;
            string email = InputHelper.GetNonEmptyString("Enter parent's email:");

            var parent = new Parent
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phone,
                Email = email
            };

            business.Add(parent);

            Console.Clear();
            ShowMenu();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Parent added successfully.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public override void ListAll()
        {
            Console.Clear();
            ShowMenu();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(new string('-', 60));
            Console.WriteLine(@"╔═╗╔═╗╦═╗╔═╗╔╗╔╔╦╗╔═╗                                       
╠═╝╠═╣╠╦╝║╣ ║║║ ║ ╚═╗                                       
╩  ╩ ╩╩╚═╚═╝╝╚╝ ╩ ╚═╝                                       ");
            Console.WriteLine(new string('-', 60));
            var items = business.GetAll();
            if(items.Count == 0)
                Console.WriteLine("No parents found      ");
            else
                foreach (var item in items)
                    Console.WriteLine(item);
            Console.WriteLine(new string('-', 60));
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
            //Parent parent = business.Get(id);
            //if (parent != null)
            //{
            //    Console.WriteLine("Enter First Name: ");
            //    parent.FirstName = Console.ReadLine();               
            //    Console.WriteLine("Enter Last Name: ");
            //    parent.LastName = Console.ReadLine();
            //    Console.WriteLine("Ënter Phone number: ");
            //    parent.PhoneNumber = Console.ReadLine();
            //    Console.WriteLine("Enter email: ");
            //    parent.Email = Console.ReadLine();
            //    business.Update(parent);
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
            Parent parent = business.Get(id);

            if (parent != null)
            {
                parent.FirstName = InputHelper.GetNonEmptyString("Enter teacher's first name:");
                parent.LastName = InputHelper.GetNonEmptyString("Enter teacher's last name:");
                parent.Email = InputHelper.GetNonEmptyString("Enter teacher's email:");
                string phone = InputHelper.GetNonEmptyString("Enter teacher's phone (optional):");
                parent.PhoneNumber = string.IsNullOrWhiteSpace(phone) ? null : phone;

                business.Update(parent);

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
            Parent parent = business.Get(id);
            if (parent != null)
            {
                Console.WriteLine(parent);
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
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
            Console.Clear();
            ShowMenu();
            //int id = 0;
            //Console.WriteLine("All current parents:");
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
            //Console.WriteLine($"Parent with id:{id} deleted");
            //Console.ForegroundColor = ConsoleColor.White;
            //Console.BackgroundColor = ConsoleColor.Black;
            int id = InputHelper.GetValidInt("Enter ID to delete:");
            Parent parent = business.Get(id);

            if (parent != null)
            {
                business.Delete(id);
                Console.WriteLine($"Parent with ID: {id} deleted successfully.");
            }
            else
                Console.WriteLine($"Parent with ID: {id} not found.");
        }
    }
}
