﻿using FeitWorkshop.Models;
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
    public class StudentVM
    {
        /*
         * [Required]
        [StringLength(10)]
        [Display(Name = "Индекс")]
        public string StudentId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Презиме")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [AllowNull]
        [Display(Name = "Датум на запишување")]
        public DateTime? EnrollmentDate { get; set; }

        [Display(Name = "Вкупно кредити")]
        [AllowNull]
        public int? AcquiredCredits { get; set; }

        [Range(1, 8)]
        [Display(Name = "Моментален семестар")]
        [AllowNull]
        public int? CurrentSemestar { get; set; }

        [StringLength(25)]
        [AllowNull]
        [Display(Name = "Циклис на студии")]
        public string EducationLevel { get; set; }

        [Display(Name = "Име")]
        public string FullName
        {
            get { return String.Format("{0} {1}", FirstName, LastName); }
        }

        public ICollection<Enrollment> Courses { get; set; }

        public string ProfilePicture { get; set; }
         * 
         * 
         */


        public IList<Student> Studens { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Id { get; set; }
        //public IFormFile ProfileImage { get; set; }


        /*
        [Required]
        [StringLength(10)]
        [Display(Name = "Индекс")]
        public string StudentId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Презиме")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [AllowNull]
        [Display(Name = "Датум на запишување")]
        public DateTime? EnrollmentDate { get; set; }

        [Display(Name = "Вкупно кредити")]
        [AllowNull]
        public int? AcquiredCredits { get; set; }

        [Range(1, 8)]
        [Display(Name = "Моментален семестар")]
        [AllowNull]
        public int? CurrentSemestar { get; set; }

        [StringLength(25)]
        [AllowNull]
        [Display(Name = "Циклис на студии")]
        public string EducationLevel { get; set; }

        */
    }
}
