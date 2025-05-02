using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    //Teacher object model
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string Phone { get; set; }

        public int? SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; }

        //Conection to ClassGroup
        public ICollection<ClassGroup> Classes { get; set; }

        //Refactored ToString method for better readability
        public override string ToString()
        {
            return
                $"{"TeacherID:",-15}{Id,-51}\n" +
                $"{"First name:",-15}{FirstName,-51}\n" +
                $"{"Last name:",-15}{LastName,-51}\n" +
                $"{"Phone number:",-15}{(Phone ?? "None"),-51}\n" +
                $"{"Email:",-15}{Email,-51}\n" +
                $"{"Subject:",-15}{(Subject?.Name ?? "None"),-51}\n" +
                $"{new string(' ', 66)}";
        }
    }
}
