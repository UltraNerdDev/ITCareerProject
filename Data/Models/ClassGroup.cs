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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Добави тази анотация
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } // Например "10A"

        [Required]
        public int Year { get; set; } // Година на обучение (примерно 2025)

        // Класен ръководител
        public int? TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public Teacher Teacher { get; set; }
    }
}
