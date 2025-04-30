using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
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

        public override string ToString()               
        {
            return $"Class ID: {Id, -8}\nClass name: {Name, -8}\nClass year: {Year, -8}\n" +
                $"TeacherID: {(Teacher != null ? Teacher.FirstName : "None"), -8}";
        }
    }
}
