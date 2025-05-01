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

        public int? Age { get; set; }

        //Връзка с клас
        [Required]
        public int ClassGroupId { get; set; }
        [ForeignKey(nameof(ClassGroupId))]
        public ClassGroup ClassGroup { get; set; }

        [Required]
        public string Email { get; set; }

        public int? EnrollmentDate { get; set; }

        // Връзка с родител
        [Required]
        public int ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]

        public Parent Parent { get; set; }

        // Връзка към оценките
        public ICollection<Grade> Grades { get; set; }

        public override string ToString()
        {
            //return $"StudentID: {Id,-8}\nFirst name: {FirstName,-10}\nLast name: {LastName,-10}\n" +
            //    $"Age: {Age,-8}\nEmail: {Email,-12}\nEnrollment date: {EnrollmentDate,-12}\n" +
            //    $"ParentID: {(ParentId != null ? ParentId : "None"),-20}";
            return
                $"{"Student ID:",-25}{Id,-44}\n" +
                $"{"First name:",-25}{FirstName,-44}\n" +
                $"{"Last name:",-25}{LastName,-44}\n" +
                $"{"Age:",-25}{Age,-44}\n" +
                $"{"Email:",-25}{Email,-44}\n" +
                $"{"Enrollment date:",-25}{(EnrollmentDate != null ? EnrollmentDate : "None"),-44}\n" +
                $"{"Parent ID:",-25}{(ParentId != null ? ParentId : "None"),-44}\n" +
                $"{new string(' ', 69)}";
        }
    }
}
