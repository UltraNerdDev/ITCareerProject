using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int Age { get; set; }

        //Връзка с клас
        [Required]
        public int? ClassGroupId { get; set; }
        [ForeignKey(nameof(ClassGroupId))]
        public ClassGroup ClassGroup { get; set; }

        [Required]
        public string Email { get; set; }

        public DateOnly EnrollmentDate { get; set; }

        // Връзка с родител
        [Required]
        public int? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]

        public  Parent Parent { get; set; }

        // Връзка към оценките
        public ICollection<Grade> Grades { get; set; }

        public override string ToString()
        {
            return $"StudentID: {Id,-8}\nFirst name: {FirstName,-10}\nLast name: {LastName,-10}\n" +
                $"Age: {Age,-8}\nEmail: {Email,-12}\nEnrollment date: {EnrollmentDate,-12}\n" +
                $"ParentID: {(ParentId != null ? ParentId : "None"),-20}";
               // $"ParentID: {(Parent != null ? Parent.FirstName + " " + Parent.LastName : "None"),-20}";
        }
    }
}
