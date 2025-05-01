using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Grade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
        public int Id { get; set; }

        [Required]
        public double Value { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        public string Comment { get; set; }

        // Връзка към ученик
        [Required]
        public int StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]  
        public Student Student { get; set; }

        // Връзка към предмет
        [Required]
        public int SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; }

        // Връзка към учител
        [Required]
        public int TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public Teacher Teacher { get; set; }

        public override string ToString()
        {
            return $"GradeID: {Id,-8}\nGrade value: {Value,-8:f2}\nDate: {Date,-8}\nComment name: {Comment}\n" +
                $"Student: {(Student != null ? Student.FirstName : "none")}\nTeacher: {(Teacher != null ? Teacher.FirstName : "none")}" +
                $"\nSubject: {(Subject != null ? Subject.Name : "none")}\nTeacherID: {(Teacher != null ? Teacher.Id : "none")}";
        }
    }
}
