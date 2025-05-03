using Business;
using Data;
using Data.Models;
using SchoolRegistryConsoleApp.Presentation;
using SchoolRegistryConsoleApp.Presentation;

namespace SchoolRegistryConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var operation = -1;
            int closeOperationId = 10;

            //The program display "WELCOME" menu
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string('=', 80));
            Console.WriteLine(@"         ██     ██ ███████ ██       ██████  ██████  ███    ███ ███████ 
         ██     ██ ██      ██      ██      ██    ██ ████  ████ ██      
         ██  █  ██ █████   ██      ██      ██    ██ ██ ████ ██ █████   
         ██ ███ ██ ██      ██      ██      ██    ██ ██  ██  ██ ██      
          ███ ███  ███████ ███████  ██████  ██████  ██      ██ ███████ ");
            Console.WriteLine(new string('=', 80));
            Console.BackgroundColor = ConsoleColor.Black;

            //Wait for a key press
            Console.WriteLine("Press any key to continue...");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey(true); 

            //Check if the database is empty
            using (var context = new SchoolRegistryContext())
            {
                bool isDatabaseEmpty = !context.Parents.Any() &&
                                       !context.Students.Any() &&
                                       !context.Teachers.Any() &&
                                       !context.Classes.Any() &&
                                       !context.Subjects.Any() &&
                                       !context.Grades.Any();

                if (isDatabaseEmpty)
                {
                    Console.WriteLine("The database is empty. Would you like to use the seeder to populate it with pre-defined data? (y/n): ");
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    string input = (keyInfo.KeyChar).ToString();
                    if (input.ToLower() == "y")
                    {
                        Seeder(context);
                        Console.Clear();
                    }
                    else
                        Console.Clear();
                }
                else
                    Console.Clear();
            }

            //Reads the input and sends it to the coresponding class/method
            do
            {
                if (operation != closeOperationId)
                {
                    MainMenu();
                    Display display;
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    char pressedKey = keyInfo.KeyChar;
                    if (char.IsDigit(pressedKey))
                        operation = int.Parse(pressedKey.ToString());
                    else
                        operation = -1;  

                    switch (operation)
                    {
                        //enter the grades display
                        case 1:
                            Console.Clear();
                            display = new GradesDisplay();
                            break;
                        //enter the classes display
                        case 2:
                            Console.Clear();
                            display = new ClassGroupDisplay();
                            break;
                        //enter the parents display
                        case 3:
                            Console.Clear();
                            display = new ParentsDisplay();
                            break;
                        //enter the students display
                        case 4:
                            Console.Clear();
                            display = new StudentsDisplay();
                            break;
                        //enter the subjects display
                        case 5:
                            Console.Clear();
                            display = new SubjectDisplay();
                            break;
                        //enter the teachers display
                        case 6:
                            Console.Clear();
                            display = new TeachersDisplay();
                            break;
                        //clear the database
                        case 7:
                            using(var context = new SchoolRegistryContext())
                            {
                                context.Database.EnsureDeleted(); 
                                context.Database.EnsureCreated(); 
                                context.SaveChanges();
                                Console.WriteLine("Database cleared. Would you like to use the seeder to populate it with pre-defined data? (y/n): ");
                                ConsoleKeyInfo key = Console.ReadKey(true);
                                string input = (key.KeyChar).ToString();
                                if (input.ToLower() == "y")
                                {
                                    Seeder(context);
                                    Console.Clear();
                                }
                            }
                            break;
                        //close the application
                        case 8:
                            Environment.Exit(0);
                            break;
                        default:
                            break;
                    }
                }
                else
                    Console.Clear();
                if (operation != closeOperationId)
                    Console.Clear();
            } while (operation != closeOperationId);

        }

        //Shows the main user menu on the console
        public static void MainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(new string('=', 120));
            Console.WriteLine(@"███████  ██████ ██   ██  ██████   ██████  ██          ██████  ███████  ██████  ██ ███████ ████████ ██████  ██    ██     
██      ██      ██   ██ ██    ██ ██    ██ ██          ██   ██ ██      ██       ██ ██         ██    ██   ██  ██  ██      
███████ ██      ███████ ██    ██ ██    ██ ██          ██████  █████   ██   ███ ██ ███████    ██    ██████    ████       
     ██ ██      ██   ██ ██    ██ ██    ██ ██          ██   ██ ██      ██    ██ ██      ██    ██    ██   ██    ██        
███████  ██████ ██   ██  ██████   ██████  ███████     ██   ██ ███████  ██████  ██ ███████    ██    ██   ██    ██        ");
            Console.WriteLine(new string('=', 120));
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. Grades" + new string(' ', 111));
            Console.WriteLine("2. Classes" + new string(' ', 110));
            Console.WriteLine("3. Parents" + new string(' ', 110));
            Console.WriteLine("4. Students" + new string(' ', 109));
            Console.WriteLine("5. Subjects" + new string(' ', 109));
            Console.WriteLine("6. Teachers" + new string(' ', 109));
            Console.WriteLine("7. Clear database" + new string(' ', 103));
            Console.WriteLine("8. Close Application" + new string(' ', 100));
            using (var customQueries = new CustomQueriesBusiness())
            {
                Console.WriteLine(new string('-', 120));
                Console.WriteLine($"Total students: {customQueries.GetTotalStudents(), 3}" + new string(new string(' ', 101)));
                Console.WriteLine($"Total subjects: {customQueries.GetTotalSubjects(), 3}" + new string(new string(' ', 101)));
                Console.WriteLine($"Total teachers: {customQueries.GetTotalTeachers(), 3}" + new string(new string(' ', 101)));
                Console.WriteLine($"Total classes: {customQueries.GetTotalClasses(), 3}" + new string(new string(' ', 102)));
                Console.WriteLine($"Total parents: {customQueries.GetTotalParents(), 3}" + new string(new string(' ', 102)));
                Console.WriteLine($"Most popular subject: {customQueries.GetMostPopularSubject(), 10}" + new string(new string(' ', 88)));
                Console.WriteLine($"Teacher with most classes: {customQueries.GetTeacherWithMostClasses(), 13}" + new string(new string(' ', 80)));
                Console.WriteLine(new string('-', 120));
                Console.WriteLine("Enter a command ID: " + new string(' ', 100));
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        //Helper method for seeding the database with pre-defined data
        static void Seeder(SchoolRegistryContext context)
        {
            //Clear the database
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            //Add initial data
            //Make sure to save changes after every add of an object to ensure that the ID is generated
            var subject1 = new Subject { Name = "Mathematics" };
            var subject2 = new Subject { Name = "Physics" };
            context.Subjects.AddRange(subject1, subject2);
            context.SaveChanges();

            var parent1 = new Parent { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "123456789" };
            var parent2 = new Parent { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", PhoneNumber = "987654321" };
            context.Parents.AddRange(parent1, parent2);
            context.SaveChanges();

            var teacher1 = new Teacher { FirstName = "Alice", LastName = "Brown", Email = "alice.brown@example.com", Phone = "555123456" };
            var teacher2 = new Teacher { FirstName = "Bob", LastName = "White", Email = "bob.white@example.com", Phone = "555654321" };
            context.Teachers.AddRange(teacher1, teacher2);
            context.SaveChanges();

            var classgroup1 = new ClassGroup { Name = "Class A", Year = 2025, TeacherId = teacher1.Id };
            var classgroup2 = new ClassGroup { Name = "Class B", Year = 2026, TeacherId = teacher2.Id };
            context.Classes.AddRange(classgroup1, classgroup2);
            context.SaveChanges();

            var student1 = new Student { FirstName = "Charlie", LastName = "Johnson", Email = "charlie.johnson@example.com", ClassGroupId = classgroup1.Id, ParentId = parent1.Id };
            var student2 = new Student { FirstName = "Daisy", LastName = "Williams", Email = "daisy.williams@example.com", ClassGroupId = classgroup2.Id, ParentId = parent2.Id };
            context.Students.AddRange(student1, student2);
            context.SaveChanges();

            var grade1 = new Grade { Value = 95.0, Date = new DateOnly(2025, 5, 1), Comment = "Excellent", StudentId = student1.Id, SubjectId = subject1.Id, TeacherId = teacher1.Id };
            var grade2 = new Grade { Value = 88.0, Date = new DateOnly(2025, 5, 2), Comment = "Very Good", StudentId = student2.Id, SubjectId = subject2.Id, TeacherId = subject2.Id };
            context.Grades.AddRange(grade1, grade2);
            context.SaveChanges();
        }
    }
}
