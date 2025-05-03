using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace SchoolRegistryConsoleApp.Presentation
{
    //Display class is the base class for all of the displays in the application
    public abstract class Display
    {
        private int closeOperationId = 6;
        private int operation = -1;
        public SchoolRegistryContext context;

        public Display()
        {
            context = new SchoolRegistryContext(); //Every display is provided with a context object
            Input(); //The input method is called right after the display object is created
        }

        //Shows the user menu of the given entity on the console
        public virtual void ShowMenu() { }
        //Reads the Input from the console
        public void Input()
        {
            ShowMenu();
            do
            {
                if (operation != closeOperationId)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);  
                    char pressedKey = keyInfo.KeyChar;
                    if (char.IsDigit(pressedKey))
                        operation = int.Parse(pressedKey.ToString());
                    else
                        operation = -1;
                    //^this fragment of the code is removing the need for pressing enter after the number is pressed by the user
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
                            break;
                        default:
                            Console.WriteLine("Please type a valid command id!");
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                }
            } while (operation != closeOperationId);

        }

        //Returns all of the records in the given entity
        public virtual void ListAll() { }

        //Adds a record to the given entity
        public virtual void Add() { }

        //Updates a record to the given entity by accesing it by its primary key
        public virtual void Update() { }

        //Returns a singular record in the given entity by its primary key
        public virtual void Fetch() { }

        //Deletes a singular record in the given entity by its primary key.
        public virtual void Delete() { }
    }

}
