using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Data.Models
{
    //ClassGroup object model
    public class ClassGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } 

        [Required]
        public int Year { get; set; } 

        public int? TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public Teacher Teacher { get; set; }

        //Refactored ToString method for better readability
        public override string ToString()               
        {
            return
                $"{"Class ID:",-25}{Id,-42}\n" +
                $"{"Class name:",-25}{Name,-42}\n" +
                $"{"Class teacher:",-25}{Teacher.FirstName + " " + Teacher.LastName,-42}\n" +
                $"{new string(' ', 67)}";
        }
    }
}
