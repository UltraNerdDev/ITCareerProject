using Data.Models;
using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegistryConsoleApp.Presentation
{
    //Display class for Parents, using the business layer to perform CRUD operations and enhancing the UI experience
    public class ParentsDisplay : Display
    {
        private int closeOperationId = 6;
        private ParentBusiness business = new ParentBusiness();

        public ParentsDisplay()
        {
            Input();
        }

        //Shows the user menu of the given entity on the console
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

        //Add method realising the logic of adding a new Parent object to the database with UI
        public override void Add()
        {
            Console.Clear();
            ShowMenu();

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

        //ListAll method realising the logic of listing all of the Parent objects in the database with UI
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
                Console.WriteLine("No parents found                                            ");
            else
                foreach (var item in items)
                    Console.WriteLine(item);
            Console.WriteLine(new string('-', 60));
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        //Update method realising the logic of updating an existing Parent object in the database with UI
        public override void Update()
        {
            Console.Clear();
            ShowMenu();
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

        //Fetch method realising the logic of fetching a single Parent object from the database with UI
        public override void Fetch()
        {
            Console.Clear();
            ShowMenu();
            int id = 0;
            Console.WriteLine("All current Parents:");
            ListAll();
            Console.WriteLine("Enter ID to fetch: ");
            id = InputHelper.GetValidInt("Enter ID to fetch:");
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

        //Delete method realising the logic of deleting an existing Parent object in the database with UI
        public override void Delete()
        {
            Console.Clear();
            ShowMenu();
            ListAll();

            int id = InputHelper.GetValidInt("Enter ID to delete:");
            Parent parent = business.Get(id);

            if (parent != null)
            {
                business.Delete(id);
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Parent with ID: {id} deleted successfully.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Parent with ID: {id} not found.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
    }
}
