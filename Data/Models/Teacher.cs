using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
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

        public ICollection<ClassGroup> Classes { get; set; }

        public override string ToString()
        {
            return $"TeacherID: {Id,-8}\nFirst name: {FirstName,-10}\nLast name: {LastName,-10}\n" +
                $"Phone number: {(Phone != null ? "None" : "None"),-12}\nEmail: {Email}" +
                $"\nSubject: {(Subject != null ? Subject.Name : "None")}";
        }
    }
}
