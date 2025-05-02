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
        public static string GetNonEmptyString(string prompt)
        {
            string input;
            while (true)
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(input))
                    return input;
                Console.WriteLine("Input cannot be empty. Please try again.");
            }
        }

        //Method for validating a string input used for int properties
        public static int GetValidInt(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine()?.Trim();

                if (int.TryParse(input, out int result))
                    return result;
                else
                    Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        //Method for validating a string input used for int properties (4 digit long years)
        public static int GetValidIntYear(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string value = Console.ReadLine();
                if (value.All(Char.IsDigit))
                {
                    if (value.Length != 4)
                        Console.WriteLine("Year must be 4 digits long. Please try again.");
                    else
                        return int.Parse(value);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid year.");
                }
            }
        }

        //Method for validating a foreign key input
        public static int GetValidForeignKey<T>(string prompt, Func<SchoolRegistryContext, DbSet<T>> dbSetSelector) where T : class
        {
            int id;
            while (true)
            {
                id = GetValidInt(prompt);
                using (var context = new SchoolRegistryContext())
                {
                    var dbSet = dbSetSelector(context);
                    var entity = dbSet.Find(id);
                    if (entity != null)
                        return id;

                    Console.WriteLine("Invalid ID. Please try again.");
                }
            }
        }
    }
}
