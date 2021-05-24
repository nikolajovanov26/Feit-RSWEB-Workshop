using FeitWorkshop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
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
    }
}
