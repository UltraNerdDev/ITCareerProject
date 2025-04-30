using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace SchoolRegistryConsoleApp.Presentation
{
    public abstract class Display
    {
        private int closeOperationId = 6;
        private int operation = -1;
        public SchoolRegistryContext context;

        public Display()
        {
            context = new SchoolRegistryContext();
            Input();
        }

        //Shows the user menu of the given entity on the console
        public virtual void ShowMenu() { }
        // Reads the Input from the console
        public void Input()
        {
            ShowMenu();
            do
            {
                if (operation != closeOperationId)
                {
                    //ShowMenu();
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);  // 'true' скрива натиснатия клавиш от екрана
                    char pressedKey = keyInfo.KeyChar;
                    // Преобразуваме натиснатия клавиш в int (ако е цифра)
                    if (char.IsDigit(pressedKey))
                        operation = int.Parse(pressedKey.ToString());
                    else
                        operation = -1; 
                    switch (operation)
                    {
                        case 1:
                            ListAll();
                            break;
                        case 2:
                            Add();
                            break;
                        case 3:
                            Update();
                            break;
                        case 4:
                            Fetch();
                            break;
                        case 5:
                            Delete();
                            break;
                        case 6:
                            // StartUp.MainMenu();
                            break;
                        default:
                            Console.WriteLine("Моля въведете правилна команда!");
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                }

                //if (operation != closeOperationId)
                //{
                //    Console.Clear();
                //}
            } while (operation != closeOperationId);

        }
        /// <summary>
        /// Returns all of the records in the given entity
        /// </summary>
        public virtual void ListAll() { }

        // Checks whether or not the given string in empty. If so it returns true; otherwise fa1se.
        public static bool EmptyStringChecker(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Adds a record to the given entity
        /// </summary>
        public virtual void Add() { }
        /// <summary>
        /// Updates a record to the given entity by accesing it by its primary key
        /// </summary>
        public virtual void Update() { }
        /// <summary>
        /// Returns a singular record in the given entity by its primary key
        /// </summary>
        public virtual void Fetch() { }

        /// <summary>
        /// Deletes a singular record in the given entity by its primary key.
        /// Thows an error if the operation is unsuccesful.
        /// </summary>
        public virtual void Delete() { }
    }

}
