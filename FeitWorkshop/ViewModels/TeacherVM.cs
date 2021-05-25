using FeitWorkshop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FeitWorkshop.ViewModels
{
    public class TeacherVM
    {
        public IList<Teacher> Teachers { get; set; }
        public SelectList Degree { get; set; }
        public SelectList AcademicRank { get; set; }
        public string TeacherDegree { get; set; }
        public string TeacherRank { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Презиме")]
        public string LastName { get; set; }

        [StringLength(50)]
        [AllowNull]
        [Display(Name = "Диплома")]
        public string SDegree { get; set; }

        [StringLength(25)]
        [AllowNull]
        [Display(Name = "Академски ранк")]
        public string SAcademicRank { get; set; }

        [StringLength(10)]
        [AllowNull]
        [Display(Name = "Канцеларија")]
        public string OfficeNumber { get; set; }

        [DataType(DataType.Date)]
        [AllowNull]
        [Display(Name = "Датум на вработување")]
        public DateTime? HireDate { get; set; }

        [Display(Name = "Предмети")]
        public ICollection<Course> FirstTeacher { get; set; }

        [Display(Name = "Предмети")]
        public ICollection<Course> SecondTeacher { get; set; }


        [StringLength(50)]
        public string email { get; set; }

        [StringLength(50)]
        public string password { get; set; }

        public IFormFile ProfileImage { get; set; }
    }
}
