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
    //Display class for ClassGroup, using the business layer to perform CRUD operations and enhancing the UI 
    public class ClassGroupDisplay : Display
    {
        private int closeOperationId = 6;
        private ClassGroupBusiness classBusiness = new ClassGroupBusiness();
        private TeacherBusiness teacherBusiness = new TeacherBusiness();
        

        public ClassGroupDisplay()
        {
            Input();
        }

        //Shows the main user menu of the given entity on the console
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

        //Add method realising the logic of adding a new ClassGroup object to the database with UI
        public override void Add()
        {
            Console.Clear();
            ShowMenu();          

            string name = InputHelper.GetNonEmptyString("Enter class group's name:");
            int year = InputHelper.GetValidIntYear("Enter class group's year:");

            //Displaying all avalible teachers
            Console.WriteLine("Avalible teachers:");
            var items = teacherBusiness.GetAll();
            Console.WriteLine(new string('-', 25));
            if (items.Count == 0)
                Console.WriteLine("None");
            else
                foreach (var item in items)
                    Console.WriteLine($"{item.Id,-5} {item.FirstName, 15}");
            Console.WriteLine(new string('-', 25));
            // Get valid teacher ID using InputHelper class
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

            Console.Clear();
            ShowMenu();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Class added successfully.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        //ListAll method realising the logic of listing all ClassGroup objects from the database with UI
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
                Console.WriteLine("No class groups found                                              ");
            else
                foreach (var item in items)                    
                    Console.WriteLine(item);
            Console.WriteLine(new string('-', 67));
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        //Update method realising the logic of updating an existing ClassGroup object in the database with UI
        public override void Update()
        {
            Console.Clear();
            ShowMenu();
            ListAll();

            int id = InputHelper.GetValidInt("Enter ID to update:");
            ClassGroup classGroup = classBusiness.Get(id);

            if (classGroup != null)
            {
                classGroup.Name = InputHelper.GetNonEmptyString("Enter class group's name:");
                classGroup.Year = InputHelper.GetValidInt("Enter class group's year:");

                //Displaying all avalible teachers
                Console.WriteLine("Avalible teachers:");
                var items = teacherBusiness.GetAll();
                Console.WriteLine(new string('-', 25));
                if (items.Count == 0)
                    Console.WriteLine("None");
                else
                    foreach (var item in items)
                        Console.WriteLine($"{item.Id,-5} {item.FirstName,15}");
                Console.WriteLine(new string('-', 25));
                // Get valid teacher ID using InputHelper class
                classGroup.TeacherId = InputHelper.GetValidForeignKey(
                    "Enter teacher ID:",
                    context => context.Classes
                );

                classBusiness.Update(classGroup);

                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Class with ID: {id} updated successfully.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Class with ID: {id} not found.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        //Fetch method realising the logic of fetching a single ClassGroup object from the database with UI
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
                Console.WriteLine("There is no such class to fetch, please choose valid ID");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        //Delete method realising the logic of deleting an existing ClassGroup object in the database with UI
        public override void Delete()
        {
            Console.Clear();
            ShowMenu();
            ListAll();

            int id = InputHelper.GetValidInt("Enter ID to delete:");
            ClassGroup classgroup = classBusiness.Get(id);

            if (classgroup != null)
            {
                classBusiness.Delete(id);
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Class with ID: {id} deleted successfully.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.Clear();
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Class with ID: {id} not found.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
    }
}
