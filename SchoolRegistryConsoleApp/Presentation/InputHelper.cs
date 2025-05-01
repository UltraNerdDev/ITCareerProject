using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegistryConsoleApp.Presentation
{
    public static class InputHelper
    {
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

        public static int GetValidInt(string prompt)
        {
            while (true)
            {
                int id = 0;
                Console.WriteLine(prompt);
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                char pressedKey = keyInfo.KeyChar;
                if (char.IsDigit(pressedKey))
                {
                    id = int.Parse(pressedKey.ToString());
                    return id;
                }
                else
                    Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

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
