using FeitWorkshop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeitWorkshop.ViewModels
{
    public class CourseVM
    {
        public IList<Course> Courses { get; set; }
        public SelectList Semester { get; set; }
        public SelectList Program { get; set; }
        public int CourseSemester { get; set; }
        public string CourseProgram { get; set; }
        public string SearchString { get; set; }
    }
}
