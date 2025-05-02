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
    //Parent object model
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

        //Connection to Student
        public ICollection<Student> Students { get; set; }

        //Refactored ToString method for better readability
        public override string ToString()
        {
            return
                $"{"Parent ID:",-25}{Id,-35}\n" +
                $"{"First name:",-25}{FirstName,-35}\n" +
                $"{"Last name:",-25}{LastName,-35}\n" +
                $"{"Phone number:",-25}{(PhoneNumber != null ? PhoneNumber : "None"),-35}\n" +
                $"{"Email:",-25}{Email,-35}\n" +
                $"{new string(' ', 60)}";
        }
    }
}
