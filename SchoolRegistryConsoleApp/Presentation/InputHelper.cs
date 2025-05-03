using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegistryConsoleApp.Presentation
{
    //Input helper class adding methods for reading input, thus making the code more readable and reusable
    public static class InputHelper
    {
        //Method for reading a non-empty string from the console
        public static string GetNonEmptyString(string prompt) //Providing the method with error message
        {
            string input;
            while (true)
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine()?.Trim(); //Reading the user input
                if (!string.IsNullOrWhiteSpace(input)) //Checking if the input is not empty
                    return input; //If empty, the loop continues until the correct input
                Console.WriteLine("Input cannot be empty. Please try again.");
            }
        }

        //Method for validating a string input used for int properties
        public static int GetValidInt(string prompt) //Providing the method with error message
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine()?.Trim(); //Reading the user input

                if (int.TryParse(input, out int result)) //Checking if the input is a valid integer
                    return result;
                else //If empty, the loop continues until the correct input
                    Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        //Method for validating a string input used for int properties (4 digit long years)
        public static int GetValidIntYear(string prompt) //Providing the method with error message
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string value = Console.ReadLine(); //Reading the user input
                if (value.All(Char.IsDigit)) //Checking if the input is a valid integer
                {
                    if (value.Length != 4) //Checking if the input is 4 digits long
                        Console.WriteLine("Year must be 4 digits long. Please try again.");
                    else
                        return int.Parse(value);
                }
                else //If empty or incorrect, the loop continues until the correct input
                {
                    Console.WriteLine("Invalid input. Please enter a valid year.");
                }
            }
        }

        //Method for validating a foreign key input
        public static int GetValidForeignKey<T>(string prompt, Func<SchoolRegistryContext, DbSet<T>> dbSetSelector) where T : class
            //^Providing the method with error message
            //^Providing the method with a selector function for the DbSet
        {
            int id;
            while (true)
            {
                id = GetValidInt(prompt); //Reading the user input
                using (var context = new SchoolRegistryContext()) //
                {
                    //Using the selector function to get the DbSet
                    //^This allows for more flexibility in the code, as we can use this method for any DbSet
                    {
                        var dbSet = dbSetSelector(context);
                        var entity = dbSet.Find(id);
                        if (entity != null) //Checking whether the entity with the given ID exists
                            return id; //If so, the ID is returned

                        //If not, the loop continues until the correct input
                        Console.WriteLine("Invalid ID. Please try again.");
                    }
                }
            }
        }
    }
}