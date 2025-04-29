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

            // Reads the input and sends it to the coresponding class/method
            do
            {
                if (operation != closeOperationId)
                {
                    MainMenu();
                    SubjectDisplay display;
                    //operation = int.Parse(Console.ReadLine());
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);  // 'true' скрива натиснатия клавиш от екрана
                    char pressedKey = keyInfo.KeyChar;


                    // Преобразуваме натиснатия клавиш в int (ако е цифра)
                    if (char.IsDigit(pressedKey))
                        operation = int.Parse(pressedKey.ToString());
                    else
                        operation = -1;  // Ако не е цифра, не правим нищо

                    //Console.Beep();
                    switch (operation)
                    {
                        case 1:
                            Console.Clear();
                            //display = new GradesDisplay();
                            break;
                        case 2:
                            Console.Clear();
                            //display = new ClassesDisplay();
                            break;
                        case 3:
                            Console.Clear();
                            //display = new ParentsDisplay();
                            break;
                        case 4:
                            Console.Clear();
                            //display = new StudentsDisplay();
                            break;
                        case 5:
                            Console.Clear();
                            display = new SubjectDisplay();
                            break;
                        case 6:
                            Console.Clear();
                            //display = new TeachersDisplay();
                            break;
                        case 7:
                            Environment.Exit(0);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                }
                if (operation != closeOperationId)
                {
                    //Console.WriteLine("Press any key...");
                    //Console.ReadKey();
                    Console.Clear();
                }
            } while (operation != closeOperationId);

        }

        // Shows the main user menu on the console
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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. Grades" + new string(' ', 111));
            Console.WriteLine("2. Classes" + new string(' ', 110));
            Console.WriteLine("3. Parents" + new string(' ', 110));
            Console.WriteLine("4. Students" + new string(' ', 109));
            Console.WriteLine("5. Subjects" + new string(' ', 109));
            Console.WriteLine("6. Teachers" + new string(' ', 109));
            Console.WriteLine("7. Close Application" + new string(' ', 100));
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
    
}
