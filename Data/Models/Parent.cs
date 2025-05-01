using Azure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Parent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string? PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        // Връзка към ученици
        public ICollection<Student> Students { get; set; }
        public override string ToString()
        {
            //return $"ParentID: {Id,-8}\nFirst name: {FirstName, -10}\nLast name: {LastName,-10}\n" +
            //    $"Phone number: {(PhoneNumber != null ? PhoneNumber : "None"),-12}\nEmail: {Email}";
            return
                $"{"Parent ID:",-25}{Id,-30}\n" +
                $"{"First name:",-25}{FirstName,-30}\n" +
                $"{"Last name:",-25}{LastName,-30}\n" +
                $"{"Phone number:",-25}{(PhoneNumber != null ? PhoneNumber : "None"),-30}\n" +
                $"{"Email:",-25}{Email,-30}\n" +
                $"{new string(' ', 55)}";
        }
    }
}
