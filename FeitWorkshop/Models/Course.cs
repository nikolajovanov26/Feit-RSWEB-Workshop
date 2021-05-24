using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FeitWorkshop.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Име")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Кредити")]
        public int Credits { get; set; }

        [Required]
        [Display(Name = "Семестар")]
        [Range(1, 8)]
        public int Semester { get; set; }

        [StringLength(100)]
        [Display(Name = "Програма")]
        public string Programme { get; set; }

        [Display(Name = "Циклус на студии")]
        [AllowNull]
        [StringLength(25)]
        public string EducationLevel { get; set; }


        [Display(Name = "Професор 1")]
        [AllowNull]
        public int? FirstTeacherId { get; set; }


        [Display(Name = "Професор 1")]
        [AllowNull]
        public Teacher FirstTeacher { get; set; }


        [Display(Name = "Професор 2")]
        [AllowNull]
        public int? SecondTeacherId { get; set; }


        [Display(Name = "Професор 2")]
        [AllowNull]
        public Teacher SecondTeacher { get; set; }


        [Display(Name = "Студенти")]
        public ICollection<Enrollment> Students { get; set; }
    }
}
