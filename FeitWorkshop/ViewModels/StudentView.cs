using FeitWorkshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeitWorkshop.ViewModels
{
    public class StudentView
    {
        public Student Student { get; set; }

        public ICollection<Enrollment> Courses { get; set; }


        public int Grade { get; set; }

        public int Points { get; set; }

    }
}
