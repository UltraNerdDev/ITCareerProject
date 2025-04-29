﻿using System;
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Добави тази анотация
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string Phone { get; set; }

        // Преподаващ предмет
        public int? SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; }

        // Връзка към клас (ако е класен ръководител)
        public ICollection<ClassGroup> Classes { get; set; }
    }
}
