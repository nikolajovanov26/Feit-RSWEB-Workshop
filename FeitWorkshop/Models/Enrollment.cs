using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FeitWorkshop.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Предмет")]
        public int? CourseId { get; set; }
        public Course Course { get; set; }

        [Required]
        [Display(Name = "Индекс")]
        public int? StudentId { get; set; }
        public Student Student { get; set; }

        [Range(1, 8)]
        [AllowNull]
        [Display(Name = "Семестар")]
        public int? Semester { get; set; }

        [Range(1, 4)]
        [AllowNull]
        [Display(Name = "Година")]
        public int? Year { get; set; }

        [Range(5, 10)]
        [AllowNull]
        [Display(Name = "Оцена")]
        public int? Grade { get; set; }

        [StringLength(255)]
        [AllowNull]
        [Display(Name = "Url на семинарска")]
        public string SeminalUrl { get; set; }

        [StringLength(255)]
        [AllowNull]
        [Display(Name = "Url на проект")]
        public string ProjectUrl { get; set; }

        [AllowNull]
        [Range(0, 100)]
        [Display(Name = "Поени од испит")]
        public int? ExamPoints { get; set; }

        [AllowNull]
        [Range(0, 100)]
        [Display(Name = "Поени од семинатска")]
        public int? SeminalPoints { get; set; }

        [AllowNull]
        [Range(0, 100)]
        [Display(Name = "Поени од проект")]
        public int? ProjectPoints { get; set; }

        [AllowNull]
        [Range(0, 100)]
        [Display(Name = "Дополнителни поени")]
        public int? AdditionalPoints { get; set; }

        [AllowNull]
        [DataType(DataType.Date)]
        [Display(Name = "Датум на запишување")]
        public DateTime? EnrollmentDate { get; set; }


        [Display(Name = "Вкупно Поени")]
        [NotMapped]
        public int? TotalPoints
        {
            get { return (ExamPoints + SeminalPoints + ProjectPoints + AdditionalPoints); }
        } 


    }
}
