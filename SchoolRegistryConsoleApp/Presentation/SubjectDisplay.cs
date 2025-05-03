using Azure;
using Business;
using Data;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegistryConsoleApp.Presentation
{
    //Display class for Subjects, using the business layer to perform CRUD operations and enhancing the UI experience
    public class SubjectDisplay : Display
    {
        private int closeOperationId = 6;
        private SubjectBusiness business = new SubjectBusiness();

        public SubjectDisplay()
        {
            Input();
        }

        //Shows the user menu of the given entity on the console
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

        //Add method realising the logic of adding a new Subject object to the database with UI
        public override void Add()
        {
            Console.Clear();
            ShowMenu();

            Console.WriteLine("Press ESC to cancel or ENTER to continue the add operation:");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Add operation canceled.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                return;
            }
            else if(keyInfo.Key == ConsoleKey.Enter)
            {
                string name = InputHelper.GetNonEmptyString("Enter subject's name:");

                var subject = new Subject
                {
                    Name = name
                };

                business.Add(subject);

                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Subject added successfully.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Invalid key pressed. Add operation canceled.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        //ListAll method realising the logic of listing all of the Subject objects in the database with UI
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
                Console.WriteLine("No subjects found       ");
            else
                foreach (var item in items)
                    Console.WriteLine($"{item.Id,-5} {item.Name,15}   ");
            Console.WriteLine(new string('-', 24));
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        //Update method realising the logic of updating an existing Subject object in the database with UI
        public override void Update()
        {
            Console.Clear();
            ShowMenu();
            Console.WriteLine("All current subjects");
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

        //Fetch method realising the logic of fetching a single Subject object from the database with UI
        public override void Fetch()
        {
            Console.Clear();
            ShowMenu();
            
            int id = InputHelper.GetValidInt("Enter ID to fetch:");
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

        //Delete method realising the logic of deleting an existing Subject object in the database with UI
        public override void Delete()
        {
            
            Console.Clear();
            ShowMenu();
            Console.WriteLine("All current subjects:");
            ListAll();

            Console.WriteLine("Press ESC to cancel or ENTER to continue the delete operation:");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            // Check if ESC is pressed
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.WriteLine("Delete operation canceled.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                return;
            }
            else if(keyInfo.Key == ConsoleKey.Enter)
            {
                int id = InputHelper.GetValidInt("Enter ID to delete:");
                Subject subject = business.Get(id);
                if (subject != null)
                {
                    business.Delete(id);
                    Console.Clear();
                    ShowMenu();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"Subject with ID: {id} deleted successfully.");
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
            else
            {
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Invalid key pressed. Delete operation canceled.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
    }
}
