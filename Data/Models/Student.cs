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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Добави тази анотация
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

        public DateTime EnrollmentDate { get; set; }

        // Връзка с родител
        [Required]
        public int? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public Parent Parent { get; set; }

        // Връзка към оценките
        public ICollection<Grade> Grades { get; set; }
    }
}
