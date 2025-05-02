using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    //Student object model
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

        [Required]
        public int ClassGroupId { get; set; }
        [ForeignKey(nameof(ClassGroupId))]
        public ClassGroup ClassGroup { get; set; }

        [Required]
        public string Email { get; set; }

        public int? EnrollmentDate { get; set; }

        [Required]
        public int ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public Parent Parent { get; set; }

        //Connection to Grades
        public ICollection<Grade> Grades { get; set; }

        //Refactored ToString method for better readability
        public override string ToString()
        {
            return
                $"{"Student ID:",-25}{Id,-44}\n" +
                $"{"First name:",-25}{FirstName,-44}\n" +
                $"{"Last name:",-25}{LastName,-44}\n" +
                $"{"Age:",-25}{(Age != null ? Age : "Empty"),-44}\n" +
                $"{"Email:",-25}{Email,-44}\n" +
                $"{"Enrollment date:",-25}{(EnrollmentDate != null ? EnrollmentDate : "None"),-44}\n" +
                $"{"Parent ID:",-25}{(ParentId != null ? ParentId : "None"),-44}\n" +
                $"{new string(' ', 69)}";
        }
    }
}
