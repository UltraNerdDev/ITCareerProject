using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    //Grade object model
    public class Grade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
        public int Id { get; set; }

        [Required]
        public double Value { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        public string? Comment { get; set; }

        [Required]
        public int StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]  
        public Student Student { get; set; }

        [Required]
        public int SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; }

        [Required]
        public int TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public Teacher Teacher { get; set; }

        //Refactored ToString method for better readability
        public override string ToString()
        {
            return
                //$"{"Grade ID:",-25}{Id,-24}\n" +
                //$"{"Value:",-25}{Value,-24}\n" +
                //$"{"Date:",-25}{Date,-24}\n" +
                //$"{"Comment:",-25}{Comment,-24}\n" +
                //$"{"Student ID:",-25}{StudentId,-24}\n" +
                //$"{"Subject ID:",-25}{SubjectId,-24}\n" +
                //$"{"Teacher ID:",-25}{TeacherId,-24}\n" +
                //$"{new string(' ', 49)}";
                $"{"Grade ID:",-25}{Id,-24}\n" +
        $"{"Value:",-25}{Value,-24}\n" +
        $"{"Date:",-25}{Date,-24}\n" +
        $"{"Comment:",-25}{(Comment ?? "None"),-24}\n" +
        $"{"Student:",-25}{(Student != null ? Student.FirstName + " " + Student.LastName : "None"),-24}\n" +
        $"{"Subject:",-25}{(Subject != null ? Subject.Name : "None"),-24}\n" +
        $"{"Teacher:",-25}{(Teacher != null ? Teacher.FirstName + " " + Teacher.LastName : "None"),-24}\n" +
        $"{new string(' ', 49)}";
        }
    }
}
