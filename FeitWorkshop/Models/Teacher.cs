using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FeitWorkshop.Models
{
    public class Teacher
    {
        public int Id { get; set; }

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
        public string Degree { get; set; }

        [StringLength(25)]
        [AllowNull]
        [Display(Name = "Академски ранк")]
        public string AcademicRank { get; set; }

        [StringLength(10)]
        [AllowNull]
        [Display(Name = "Канцеларија")]
        public string OfficeNumber { get; set; }

        [DataType(DataType.Date)]
        [AllowNull]
        [Display(Name = "Датум на вработување")]
        public DateTime? HireDate { get; set; }

        [Display(Name = "Име")]
        public string FullName
        {
            get { return String.Format("{0} {1}", FirstName, LastName); }
        }


        public ICollection<Course> FirstTeacher { get; set; }


        public ICollection<Course> SecondTeacher { get; set; }
    }
}
